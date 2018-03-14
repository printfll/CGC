#! /usr/bin/env python
# -*- coding: utf-8 -*-
import grpc
import sys
sys.path.append("./../task")
import os
from cluster import clusterservice_pb2, clusterservice_pb2_grpc, task_pb2
import parameter_pb2, moduletype_pb2
_HOST = '127.0.0.1'
_PORT = '8089'

def testgetstatus(jobname):
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = clusterservice_pb2_grpc.ClusterProxyStub(channel=conn)
    response = client.GetStatus(clusterservice_pb2.Request(id=jobname))
    print(response)

def testkilltask(jobname):
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = clusterservice_pb2_grpc.ClusterProxyStub(channel=conn)
    response = client.KillTask(clusterservice_pb2.Request(id=jobname))
    print(response)

def testgetresource():
    pass

def testsubmittask():
    conn = grpc.insecure_channel(_HOST + ':' + _PORT)
    client = clusterservice_pb2_grpc.ClusterProxyStub(channel=conn)
    cmd = "spark-submit --class org.apache.spark.examples.SparkPi --master yarn --deploy-mode cluster hdfs://sandbox:9000/user/lul/wordCount.py hdfs://sandbox:9000/user/lul/word.txt"
    response = client.SubmitTask(task_pb2.Task(command=cmd, src_code_path ="",task_type=0,config='{"config":{"cpu":1,"memory":1}}'))


if __name__ == '__main__':
    #testkilltask("application_1520476850930_0002")
    #testgetstatus("application_1520476850930_0002")
    testsubmittask()