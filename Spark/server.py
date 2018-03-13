import grpc
import time
from concurrent import futures
from cluster import clusterservice_pb2, clusterservice_pb2_grpc
from jobapi import sparkserviceapi
import logapi
import grpc
import time

import globalV

logger = logapi.Log()

class ClusterProxy(clusterservice_pb2_grpc.ClusterProxyServicer):
    # def DoFormat(self, request, context):
    #     str = request.text
    #     return ClusterProxy_pb2.Data(text=str.upper())
    # def DoAdd(selfself, request, context):
    #     str = request.text
    #     return ClusterProxy_pb2.Data(text=str.upper() + str.lower())
    def __init__(self):
        self.sparkservice = sparkserviceapi("config.txt")

    def GetResourceCapacity(self, resourcerequest, context): 
        pass

    def GetResourceAllocatable(self, resourcerequest, context):
        pass

    def GetResourcefromconfig(self, resourcerequest, context):
        pass

    def SubmitTask(self, task):
        logger.info("Calling SubmitTask service[spark].")
        res = self.sparkservice.CreateJob(task)
        logger.info("SubmitTask successfully[spark].")
        return res

    def KillTask(self, request):
        logger.info("Calling KillTask service[spark].")
        res = self.sparkservice.KillJob(request.id)
        logger.info("KillTask successfully[spark].")
        return res

    def GetStatus(self, request):
        logger.info("Calling GetStatus service[spark].")
        res = self.sparkservice.JobStatus(request.id)
        logger.info("GetStatus successfully[spark].")
        return res

    def GetLog(self, request):
        pass



def serve():
    logger.info("Openning a server in " + globalV.HOST + ":" + globalV.PORT)
    grpcServer = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    proxy = ClusterProxy()
    clusterservice_pb2_grpc.add_ClusterProxyServicer_to_server(proxy, grpcServer)
    grpcServer.add_insecure_port(globalV.HOST + ':' + globalV.PORT)
    grpcServer.start()
    try:
        while True:
            time.sleep(globalV._ONE_DAY_IN_SECONDS)
    except KeyboardInterrupt:
        grpcServer.stop(0)
