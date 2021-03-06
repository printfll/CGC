# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: jobmanager/status.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf.internal import enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='jobmanager/status.proto',
  package='grpc.jobmanager',
  syntax='proto3',
  serialized_pb=_b('\n\x17jobmanager/status.proto\x12\x0fgrpc.jobmanager*y\n\x06Status\x12\x08\n\x04NONE\x10\x00\x12\x0b\n\x07WAITING\x10\x01\x12\t\n\x05READY\x10\x02\x12\x0b\n\x07RUNNING\x10\x04\x12\x0c\n\x08\x46INISHED\x10\x08\x12\x0b\n\x07TIMEOUT\x10\x10\x12\r\n\tRERUNNING\x10 \x12\n\n\x06KILLED\x10@\x12\n\n\x05\x45RROR\x10\x80\x01\x42 \xaa\x02\x1dMicrosoft.CGC.GRPC.JobManagerb\x06proto3')
)

_STATUS = _descriptor.EnumDescriptor(
  name='Status',
  full_name='grpc.jobmanager.Status',
  filename=None,
  file=DESCRIPTOR,
  values=[
    _descriptor.EnumValueDescriptor(
      name='NONE', index=0, number=0,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='WAITING', index=1, number=1,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='READY', index=2, number=2,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='RUNNING', index=3, number=4,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='FINISHED', index=4, number=8,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='TIMEOUT', index=5, number=16,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='RERUNNING', index=6, number=32,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='KILLED', index=7, number=64,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='ERROR', index=8, number=128,
      options=None,
      type=None),
  ],
  containing_type=None,
  options=None,
  serialized_start=44,
  serialized_end=165,
)
_sym_db.RegisterEnumDescriptor(_STATUS)

Status = enum_type_wrapper.EnumTypeWrapper(_STATUS)
NONE = 0
WAITING = 1
READY = 2
RUNNING = 4
FINISHED = 8
TIMEOUT = 16
RERUNNING = 32
KILLED = 64
ERROR = 128


DESCRIPTOR.enum_types_by_name['Status'] = _STATUS
_sym_db.RegisterFileDescriptor(DESCRIPTOR)


DESCRIPTOR.has_options = True
DESCRIPTOR._options = _descriptor._ParseOptions(descriptor_pb2.FileOptions(), _b('\252\002\035Microsoft.CGC.GRPC.JobManager'))
# @@protoc_insertion_point(module_scope)
