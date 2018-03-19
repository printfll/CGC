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
    def __init__(self):
        self.sparkservice = sparkserviceapi("config/config.txt")

    def GetResourceCapacity(self, resourcerequest, context):
        pass

    def GetResourceAllocatable(self, resourcerequest, context):
        pass

    def GetResourcefromconfig(self, resourcerequest, context):
        logger.info("Calling GetResourcefromconfig service.")
        res = self.sparkservice.GetResourceFromConfig(resourcerequest.config)
        logger.info("GetResourcefromconfig successfully.")
        return res

    def SubmitTask(self, task, context):
        logger.info("Calling SubmitTask service.")
        res = self.sparkservice.CreateJob(task)
        logger.info("SubmitTask successfully.")
        return res

    def KillTask(self, request, context):
        logger.info("Calling KillTask service.")
        res = self.sparkservice.KillJob(request.id)
        logger.info("KillTask successfully.")
        return res

    def GetStatus(self, request, context):
        logger.info("Calling GetStatus service.")
        res = self.sparkservice.JobStatus(request.id)
        logger.info("GetStatus successfully.")
        return res


def serve():
    logger.info("Openning a server in " + globalV.HOST + ":" + globalV.PORT)
    grpcServer = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    proxy = ClusterProxy()
    clusterservice_pb2_grpc.add_ClusterProxyServicer_to_server(proxy, grpcServer)
    grpcServer.add_insecure_port(globalV.HOST + ':' + globalV.PORT)
    grpcServer.start()
    try:
        while True:
            time.sleep(globalV.ONE_DAY_IN_SECONDS)
    except KeyboardInterrupt:
        grpcServer.stop(0)