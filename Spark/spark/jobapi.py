from cluster import clusterservice_pb2
import status_pb2
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
        self.prefix_addr = "http://" + self.namenode + ":" + self.rm_web_addr.split(":")[-1] + "/ws/v1/cluster/apps/"
    
    def KillJob(self, jobid):
        logger.info("Calling jobapi KillJob.")
        response = clusterservice_pb2.Response()
        try:
            url = self.prefix_addr + jobid + "/state"
            data = {"state":"KILLED"}
            headers = {'Content-type': 'application/json'}
            res = requests.put(str(url), data = json.dumps(data), headers = headers)
            if str(res.json()["state"]) == "FAILED":
                response.result = False
            else:
                response.result = True
        except Exception as e:
            response.result = False
            logger.warning("Throw expcetion at KillJob: %s" % e)
        logger.info("KillJob end.")
        return response

    def CreateJob(self, task):
        logger.info("Calling jobapi CreateJob.")
        response = clusterservice_pb2.SubmitTaskResponse()
        try:
            cmd_arr = task.command.split()
            config = json.loads(task.config)
            for i in range(len(cmd_arr)):
                if cmd_arr[i].startswith("--master"):
                    cmd_arr[i+1] = "yarn"
                elif cmd_arr[i].startswith("--deploy-mode"):
                    cmd_arr[i+1] = "cluster"
                elif cmd_arr[i].startswith("spark-submit"):
                    cmd_arr[i] = ""
            if config["config"] != None:
                if config["config"]["cpu"] != None:
                    cpu = config["config"]["cpu"]
                    cmd_arr.append("--driver-cores")
                    cmd_arr.append(str(cpu))
                if config["config"]["memory"] != None:
                    memory = config["config"]["memory"]
                    cmd_arr.append("--driver-memory")
                    cmd_arr.append(str(memory))
            bashCommand = "spark-submit --conf spark.yarn.submit.waitAppCompletion=false"
            command = bashCommand + " " + " ".join(cmd_arr)
            pro = subprocess.Popen(command.split(), stdout=subprocess.PIPE)
            for line in pro.stdout:
                line = line.decode("utf-8")
                if "Submitted application" in line:
                    start = line.find("Submitted application")+len("Submitted application")
                    app_id = line[start:-1].strip()
                    response.task_id = app_id
                    response.result = True
                    break
        except Exception as e:
            response.result = False
            logger.warning("Throw expcetion at CreateJob: %s" % e)
        logger.info("CreateJob end.")
        return response


    def JobStatus(self, jobid):
        logger.info("Calling jobapi JobStatus.")
        response = clusterservice_pb2.GetStatusResponse()
        try:
            url = self.prefix_addr + jobid + "/"
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
            response.log = res.text
            return response
        except Exception as e:
            logger.warning("Throw expcetion at JobStatus: %s" % e)
            response.result = False
            response.status = status_pb2.Status.Value('ERROR')
            response.log = str(e)
        logger.info("JobStatus end.")
        return response

    def GetResourceFromConfig(self, configjson):
        try:
            data = json.loads(configjson)

        except Exception as e:
            return None