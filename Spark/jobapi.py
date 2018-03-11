from cluster import clusterservice_pb2
import status_pb2
import globalV
import subprocess
import json

class sparkserviceapi:

    def __init__(self, config):
        self.rm_web_addr = config['spark']['yarn.resourcemanager.webapp.address']
        self.namenode = config['spark']['namenode']
        self.prefix_addr = "http://" + self.namenode + ":" + self.rm_web_addr.split(":")[-1] + "/ws/v1/cluster/apps/"
    
    def KillJob(self, jobid):
        response = clusterservice_pb2.Response()
        try:
            url = self.prefix_addr + jobid + "/state"
            data = {"state":"KILLED"}
            headers = {'Content-type': 'application/json'}
            res = requests.put(str(url), data = json.dumps(data), headers = headers)
            if str(res.json()["state"]) == "FAILED":
                response["result"] = False
            else:
                response["result"] = True
        except Exception as e:
            response["result"] = False
            print("exception:",e)
        return response

    def CreateJob(self, config):
        response = clusterservice_pb2.SubmitTaskResponse()
        try:
            cmd_arr = config["command"].split()
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
                    cmd_arr.append(cpu)
                if config["config"]["memory"] != None:
                    memory = config["config"]["memory"]
                    cmd_arr.append("--driver-memory")
                    cmd_arr.append(memory)
            
            bashCommand = "spark-submit --conf spark.yarn.submit.waitAppCompletion=false"

            command = bashCommand + " " + " ".join(cmd_arr)
            print("command:",command)
            pro = subprocess.Popen(command.split(), stdout=subprocess.PIPE)
            for line in pro.stdout:
                if "Submitted application" in line:
                    print(line)
                    start = line.find("Submitted application")+len("Submitted application")
                    app_id = line[start:-1].strip()
                    response["task_id"] = app_id
                    response["result"] = True
        except Exception as e:
            response["result"] = e
            print("exception:",e)
        return response


    def JobStatus(self, jobid, namespaced=globalV.NAMESPACED):
        response = clusterservice_pb2.GetStatusResponse()
        try:
            url = self.prefix_addr + jobid + "/"
            res = requests.get(url)
            status = res.json()
            response["result"] = True
            response["status"] = str(status['app']['state'])
            response["log"] = res.text
            return response
        except Exception as e:
            response["result"] = False
            response["status"] = str(status['app']['state'])
            response["log"] = str(e)
        return response

    def GetResourceFromConfig(self, configjson):
        try:
            data = json.loads(configjson)

        except Exception as e:
            return None