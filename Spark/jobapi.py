from cluster import clusterservice_pb2
import status_pb2
import globalV
import subprocess
import json

class sparkserviceapi:

    def __init__(self, config):
        self.rm_web_addr = config['spark']['yarn.resourcemanager.webapp.address']
        self.namenode = config.spark.namenode

    def KillJob(self, jobid):
        response = clusterservice_pb2.Response()
        try:
            bashCommand = "curl -v -X PUT -H \"Content-Type: application/json\" -d '{\"state\":\"KILLED\"}' 'http://sandbox:8088/ws/v1/cluster/apps/" + jobid + "/state'"
            pro = subprocess.Popen(bashCommand, stdout=subprocess.PIPE)
            out, err = pro.communicate()
            status = json.loads(out)
            if status['state'] != "FAILED":
                response.result = True
        except Exception as e:
            response.result = False

        return response

    def CreateJob(self, command):
        response = clusterservice_pb2.SubmitTaskResponse()
        try:
            bashCommand = "spark-submit --conf spark.yarn.submit.waitAppCompletion=false"
            if command.startswith("spark-submit"):
                command = command[len("spark-submit")]
            command = bashCommand + " " + command
            pro = subprocess.Popen(command, stdout=subprocess.PIPE)
            while True:
                line = pro.stdout.readline()
                if "Submitted application" in line:
                    start = line.find("Submitted application")+len("Submitted application")
                    app_id = line[start:-1].strip()
                    response.task_id = app_id
                    response.result = True
        except Exception as e:
            response.result = False
        
        return response


    def JobStatus(self, jobid, namespaced=globalV.NAMESPACED):
        response = clusterservice_pb2.GetStatusResponse()
        try:
            web_addr = self.namenode + self.rm_web_addr.split(":")[-1]
            bashCommand = "curl http://" + web_addr + "/ws/v1/cluster/apps" + jobid 
            pro = subprocess.Popen(bashCommand, stdout=subprocess.PIPE)
            out, _ = pro.communicate()
            status = json.loads(out)
            response.result = True
            response.status = status['app']['state']
            response.log = out
            return response
        except Exception as e:
            response.result = False
            response.status = status['app']['state']
            response.log = str(e)

        return response

    def GetResourceFromConfig(self, configjson):
        try:
            data = json.loads(configjson)

        except Exception as e:
            return None