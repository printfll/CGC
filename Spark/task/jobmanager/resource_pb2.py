# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: jobmanager/resource.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='jobmanager/resource.proto',
  package='grpc.jobmanager',
  syntax='proto3',
  serialized_pb=_b('\n\x19jobmanager/resource.proto\x12\x0fgrpc.jobmanager\"\x87\x01\n\x08Resource\x12\x34\n\x04type\x18\x01 \x01(\x0e\x32&.grpc.jobmanager.Resource.ResourceType\x12\r\n\x05value\x18\x02 \x01(\x03\"6\n\x0cResourceType\x12\x07\n\x03\x43PU\x10\x00\x12\x07\n\x03GPU\x10\x01\x12\x08\n\x04\x44ISK\x10\x02\x12\n\n\x06MEMORY\x10\x03\x42 \xaa\x02\x1dMicrosoft.CGC.GRPC.JobManagerb\x06proto3')
)



_RESOURCE_RESOURCETYPE = _descriptor.EnumDescriptor(
  name='ResourceType',
  full_name='grpc.jobmanager.Resource.ResourceType',
  filename=None,
  file=DESCRIPTOR,
  values=[
    _descriptor.EnumValueDescriptor(
      name='CPU', index=0, number=0,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='GPU', index=1, number=1,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='DISK', index=2, number=2,
      options=None,
      type=None),
    _descriptor.EnumValueDescriptor(
      name='MEMORY', index=3, number=3,
      options=None,
      type=None),
  ],
  containing_type=None,
  options=None,
  serialized_start=128,
  serialized_end=182,
)
_sym_db.RegisterEnumDescriptor(_RESOURCE_RESOURCETYPE)


_RESOURCE = _descriptor.Descriptor(
  name='Resource',
  full_name='grpc.jobmanager.Resource',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='type', full_name='grpc.jobmanager.Resource.type', index=0,
      number=1, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='grpc.jobmanager.Resource.value', index=1,
      number=2, type=3, cpp_type=2, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
    _RESOURCE_RESOURCETYPE,
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=47,
  serialized_end=182,
)

_RESOURCE.fields_by_name['type'].enum_type = _RESOURCE_RESOURCETYPE
_RESOURCE_RESOURCETYPE.containing_type = _RESOURCE
DESCRIPTOR.message_types_by_name['Resource'] = _RESOURCE
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

Resource = _reflection.GeneratedProtocolMessageType('Resource', (_message.Message,), dict(
  DESCRIPTOR = _RESOURCE,
  __module__ = 'jobmanager.resource_pb2'
  # @@protoc_insertion_point(class_scope:grpc.jobmanager.Resource)
  ))
_sym_db.RegisterMessage(Resource)


DESCRIPTOR.has_options = True
DESCRIPTOR._options = _descriptor._ParseOptions(descriptor_pb2.FileOptions(), _b('\252\002\035Microsoft.CGC.GRPC.JobManager'))
# @@protoc_insertion_point(module_scope)
