import socket
from time import sleep
clientsocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clientsocket.connect(('52.234.227.186', 8087))
hdfs_path ="hdfs://sandbox:9000/user/lul/test.txt"
while True:
    try:
        clientsocket.send(hdfs_path)
        sleep(10)
        reply = clientsocket.recv(1000)
        if not reply:
            break
        print("recvd: ", reply)
    except KeyboardInterrupt:
        print("bye")
        break
clientsocket.close()

