import grpc
import time
from concurrent import futures
from cluster import clusterservice_pb2, clusterservice_pb2_grpc
from jobapi import sparkserviceapi
import globalV


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

    def SubmitTask(self, task, context):
        pass

    def KillTask(self, request, context):
        print(request)
        return self.sparkservice.KillJob(request.id)

    def GetStatus(self, request, context):
        return self.sparkservice.JobStatus(request.id)

    def GetLog(self, request, context):
        pass



def serve(_HOST, _PORT):
    grpcServer = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    proxy = ClusterProxy()
    clusterservice_pb2_grpc.add_ClusterProxyServicer_to_server(proxy, grpcServer)
    grpcServer.add_insecure_port(_HOST + ':' + _PORT)
    grpcServer.start()
    try:
        while True:
            time.sleep(globalV._ONE_DAY_IN_SECONDS)
    except KeyboardInterrupt:
        grpcServer.stop(0)
