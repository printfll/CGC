from hdfs3 import HDFileSystem
import sys
import socket

class HDFSService:
    client = None
    def __init__(self, hostname, port):
        try:
            client = HDFileSystem(hostname=hostname, port=port)
        except Exception as error:
            raise Exception("init hdfs client error:"+repr(error))

    def download(self, src, dst):
        try:
            client.cp(src, dst)
        except Exception as error:
            raise Exception("download error:"+repr(error))

    def upload(self, src, dst):
        try:
            client.put(src, dst)
        except Exception as error:
            raise Exception("upload error:"+repr(error)) 

if __name__ == '__main__':
    serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    serversocket.bind(('localhost', 9000))
    serversocket.listen(5) # become a server socket, maximum 5 connections

    while True:
        connection, address = serversocket.accept()
        buf = connection.recv(1000)
        if len(buf) > 0:
            print(buf)
            break