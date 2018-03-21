from cluster import cluster_service_pb2
from jobmanager import status_pb2, resource_pb2
import globalV
import subprocess
import json
import logapi
import requests
logger = logapi.Log()

class sparkserviceapi:
    def __init__(self, config_file):
        with open(config_file) as json_data:
            config = json.load(json_data)
        self.rm_web_addr = config['spark']['yarn.resourcemanager.webapp.address']
        self.namenode = config['spark']['namenode']
        self.prefix_addr = "http://" + self.namenode + ":" + self.rm_web_addr.split(":")[-1] + "/ws/v1/cluster/"
    
    def KillJob(self, jobid):
        logger.info("Calling jobapi KillJob.")
        response = cluster_service_pb2.Response()
        try:
            url = self.prefix_addr +"apps/" + jobid + "/state"
            data = {"state":"KILLED"}
            headers = {'Content-type': 'application/json'}
            res = requests.put(str(url), data = json.dumps(data), headers = headers)
            if str(res.json()["state"]) == "FAILED":
                response.result = False
            else:
                response.result = True
        except Exception as e:
            response.result = False
            logger.exception("Throw expcetion at KillJob: %s" % e)
        logger.info("KillJob end.")
        return response

    def CreateJob(self, task):
        logger.info("Calling jobapi CreateJob.")
        response = cluster_service_pb2.SubmitTaskResponse()
        try:
            cmd_arr = task.command.split()
            config = json.loads(task.config)
            core_set = False
            mem_set = False
            for i in range(len(cmd_arr)):
                if cmd_arr[i].startswith("--master") or cmd_arr[i].startswith("--deploy-mode"):
                    cmd_arr[i+1] = ""
                    cmd_arr[i] = ""
                elif cmd_arr[i].startswith("spark-submit"):
                    cmd_arr[i] = ""
                elif cmd_arr[i].startswith("--driver-cores") and config["cpu"]:
                    cmd_arr[i+1] = str(config["cpu"])
                    core_set = True
                elif cmd_arr[i].startswith("--driver-memory") and config["memory"]:
                    cmd_arr[i+1] = str(config["memory"])
                    mem_set = True
            
            if not core_set and config["cpu"]:
                cpu = config["cpu"]
                cmd_arr.append("--driver-cores")
                cmd_arr.append(str(cpu))
            if not mem_set and config["memory"]:
                memory = config["memory"]
                cmd_arr.append("--driver-memory")
                cmd_arr.append(str(memory))

            bashCommand = "spark-submit --conf spark.yarn.submit.waitAppCompletion=false --master yarn --deploy-mode cluster"
            command = bashCommand + " " + " ".join(cmd_arr)
            pro = subprocess.Popen(command.split(), stdout=subprocess.PIPE)
            for line in pro.stdout:
                line = line.decode("utf-8")
                if "Submitted application" in line:
                    start = line.find("Submitted application")+len("Submitted application")
                    app_id = line[start:-1].strip()
                    response.internal_task_id = app_id
                    response.result = True
                    break
        except Exception as e:
            response.result = False
            logger.exception("Throw expcetion at CreateJob: %s" % e)
        logger.info("CreateJob end.")
        return response


    def JobStatus(self, jobid):
        logger.info("Calling jobapi JobStatus.")
        response = cluster_service_pb2.GetStatusResponse()
        try:
            url = self.prefix_addr + "apps/" + jobid + "/"
            res = requests.get(url)
            status = res.json()
            response.result = True
            s = str(status['app']['state'])
            if s == "FAILED":
                response.result = status_pb2.Status.Value('ERROR')
            elif s == "RUNNING":
                response.result = status_pb2.Status.Value('RUNNING')
            elif s == "FINISHED":
                response.result = status_pb2.Status.Value('FINISHED')
            elif s == "KILLED":
                response.result = status_pb2.Status.Value('KILLED')
            elif s == "ACCEPTED":
                response.result = status_pb2.Status.Value('WAITING')
            else:
                response.result = status_pb2.Status.Value('NONE')
            response.log = res.text
        except Exception as e:
            logger.exception("Throw expcetion at JobStatus: %s" % e)
            response.result = False
            response.status = status_pb2.Status.Value('ERROR')
            response.log = str(e)
        logger.info("JobStatus end.")
        return response

    def GetResourceFromConfig(self, configjson):
        logger.info("Calling jobapi GetResourceFromConfig.")
        response = cluster_service_pb2.GetResourceResponse()
        try:
            data = json.loads(configjson)
            if data["config"]:
                response.result = True
                if data["config"]["cpu"]:
                    response.resources.add(
                        type=resource_pb2.Resource.ResourceType.Value('CPU'),
                        value=data["config"]["cpu"]
                    )
                if data["config"]["memory"]:
                    response.resources.add(
                        type=resource_pb2.Resource.ResourceType.Value('MEMORY'),
                        value=data["config"]["memory"]
                    )
        except Exception as e:
            logger.exception("Throw expcetion at GetResourceFromConfig: %s" % e)
        logger.info("GetResourceFromConfig end.")
        return response
    
    def GetSparkResource(self):
        logger.info("Calling jobapi GetSparkResource.")
        response = {}
        try:
            url =  self.prefix_addr+"/metrics"
            headers = {'Content-type': 'application/json'}
            res = requests.put(str(url), headers = headers)
            if res["clusterMetrics"]:
                response.result=True
                allocatedMB = res["clusterMetrics"]["allocatedMB"]
                totalMB = res["clusterMetrics"]["totalMB"]
                allocatedVirtualCores = res["clusterMetrics"]["allocatedVirtualCores"]
                totalVirtualCores = res["clusterMetrics"]["totalVirtualCores"]
        except Exception as e:
            logger.exception("Throw expcetion at GetSparkResource: %s" % e)
        logger.info("GetSparkResource end.")
        return response

