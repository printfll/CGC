FROM centos:centos7
WORKDIR /sparkservice
ADD . /sparkservice
RUN yum install -y https://centos7.iuscommunity.org/ius-release.rpm 
RUN yum install -y python36u python36u-pip java-1.7.0-openjdk which
RUN pip3.6 install protobuf==3.5.2 grpcio==1.10.0 grpcio-tools==1.10.0 requests==2.18.4
ENV JAVA_HOME /usr/lib/jvm/java-1.7.0-openjdk-1.7.0.171-2.6.13.0.el7_4.x86_64/jre/
RUN curl https://archive.apache.org/dist/spark/spark-1.6.0/spark-1.6.0-bin-hadoop2.6.tgz -o spark-1.6.0-bin-hadoop2.6.tgz && tar -xvf spark-1.6.0-bin-hadoop2.6.tgz -C /sparkservice/ && rm spark-1.6.0-bin-hadoop2.6.tgz
ENV SPARK_HOME /sparkservice/spark-1.6.0-bin-hadoop2.6
RUN mkdir /sparkservice/hadoop_conf_dir
ENV HADOOP_CONF_DIR /sparkservice/hadoop_conf_dir
ENV HADOOP_USER_NAME client
RUN curl https://archive.apache.org/dist/hadoop/core/hadoop-2.6.0/hadoop-2.6.0.tar.gz -o hadoop-2.6.0.tar.gz && tar -xvf hadoop-2.6.0.tar.gz -C /sparkservice/ && rm hadoop-2.6.0.tar.gz
ENV HADOOP_HOME /sparkservice/hadoop-2.6.0
ENV PATH $PATH:$SPARK_HOME/bin:$HADOOP_HOME/bin
RUN yum clean all
CMD python3.6 run.py