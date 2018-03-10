pwd
SolutionDir=../
ProjectDir=./
PROTODIR=${SolutionDir}../proto/
PROTOC=${PROTODIR}linux_x64/protoc
PROTOGRPC=${PROTODIR}linux_x64/grpc_csharp_plugin
for file in ${PROTODIR}*.proto; do echo ${file} && ${PROTOC} -I=${PROTODIR} --csharp_out=Service --grpc_out=Service ${file} --plugin=protoc-gen-grpc=${PROTOGRPC}; done