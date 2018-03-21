FROM centos:centos7
WORKDIR /sparkservice
ADD . /sparkservice
RUN yum install -y https://centos7.iuscommunity.org/ius-release.rpm
RUN yum install -y python36u
RUN yum install -y python36u-pip
RUN pip3.6 install protobuf==3.5.2
RUN pip3.6 install grpcio==1.10.0
RUN pip3.6 install grpcio-tools==1.10.0
RUN pip3.6 install requests==2.18.4
RUN yum install -y java-1.8.0-openjdk
ENV JAVA_HOME /usr/lib/jvm/java-1.8.0-openjdk-1.8.0.161-0.b14.el7_4.x86_64/jre/
RUN curl http://ftp.jaist.ac.jp/pub/apache/spark/spark-2.3.0/spark-2.3.0-bin-hadoop2.6.tgz -o spark-2.3.0-bin-hadoop2.6.tgz
RUN tar -xvf spark-2.3.0-bin-hadoop2.6.tgz -C /sparkservice/ 
ENV SPARK_HOME /sparkservice/spark-2.3.0-bin-hadoop2.6
ENV PATH $PATH:$SPARK_HOME/bin
RUN mkdir /sparkservice/hadoop_conf_dir
ENV HADOOP_CONF_DIR /sparkservice/hadoop_conf_dir
ENV HADOOP_USER_NAME client
CMD python3.6 run.py
