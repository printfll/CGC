FROM python:3.6
WORKDIR /sparkservice
ADD . /sparkservice
RUN pip install protobuf==3.5.2
RUN pip install grpcio==1.10.0
RUN pip install grpcio-tools==1.10.0
RUN yum install -y java-1.8.0-openjdk
ENV JAVA_HOME $(readlink -f /usr/bin/java | sed "s:bin/java::") 
RUN wget http://ftp.jaist.ac.jp/pub/apache/spark/spark-2.3.0/spark-2.3.0-bin-hadoop2.6.tgz | tar -xvf -C /sparkservice/
RUN ln -s /sparkservice/spark-2.3.0-bin-hadoop2.6 spark
ENV SPARK_HOME /sparkservice/spark
ENV PATH $PATH:$SPARK_HOME/bin
RUN mkdir /sparkservice/hadoop_conf_dir
ENV HADOOP_CONF_DIR /sparkservice/hadoop_conf_dir
CMD python run.py