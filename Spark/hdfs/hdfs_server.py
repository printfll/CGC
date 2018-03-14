from hdfs3 import HDFileSystem
import sys
import socket

class HDFSService:
    def __init__(self, hostname, port):
        try:
            this.client = HDFileSystem(host=hostname, port=port)
        except Exception as error:
            raise Exception("init hdfs client error:"+repr(error))

    def download(self, src, dst):
        try:
            this.client.cp(src, dst)
        except Exception as error:
            raise Exception("download error:"+repr(error))

    def upload(self, src, dst):
        try:
            this.client.put(src, dst)
        except Exception as error:
            raise Exception("upload error:"+repr(error)) 
    
    def read(self, src):
        try:
            with this.client.open(src) as f:
                content = f.read()
            return content
        except Exception as error:
            raise Exception("read error:"+repr(error)) 

if __name__ == '__main__':
    serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    serversocket.bind(('0.0.0.0', 8087))
    serversocket.listen(5) # become a server socket, maximum 5 connections
    service = HDFSService("sandbox",9000)
    while True:
        try:
            connection, address = serversocket.accept()
            buf = connection.recv(1000)
            if len(buf) > 0:
                print(buf)
            res = service.read(str(buf))
            print(res)
            serversocket.sendto(res,(address[0], address[1]))
        except Exception as error:
            raise Exception("server error:"+repr(error)) 

