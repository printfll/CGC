#! /usr/bin/env python
# -*- coding: utf-8 -*-
import grpc
import sys
sys.path.append("./../task")
import os
import json
from cluster import cluster_service_pb2, cluster_service_pb2_grpc, task_pb2
from jobmanager import parameter_pb2, module_type_pb2
_HOST = '127.0.0.1'
_PORT = '8089'

def testgetstatus(jobname):
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = cluster_service_pb2_grpc.ClusterProxyStub(channel=conn)
    response = client.GetStatus(cluster_service_pb2.Request(id=jobname))
    print(response)

def testkilltask(jobname):
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = cluster_service_pb2_grpc.ClusterProxyStub(channel=conn)
    response = client.KillTask(cluster_service_pb2.Request(id=jobname))
    print(response)

def testgetresource(config_file):
    pass

def testsubmittask(config_file):
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = cluster_service_pb2_grpc.ClusterProxyStub(channel=conn)
    data = json.load(open(config_file))
    response = client.SubmitTask(task_pb2.Task(command=data["command"], src_code_path ="",task_type=0,config=json.dumps(data["config"])))
    print(response)
    return response

if __name__ == '__main__':
    res = testsubmittask("test_config.json")
    id_ = res.internal_task_id
    testgetstatus(id_)
    testkilltask(id_)
    