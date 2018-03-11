# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: cluster/clusterservice.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()


from cluster import task_pb2 as cluster_dot_task__pb2
import status_pb2 as status__pb2


DESCRIPTOR = _descriptor.FileDescriptor(
  name='cluster/clusterservice.proto',
  package='cluster',
  syntax='proto3',
  serialized_pb=_b('\n\x1c\x63luster/clusterservice.proto\x12\x07\x63luster\x1a\x12\x63luster/task.proto\x1a\x0cstatus.proto\"\x15\n\x07Request\x12\n\n\x02id\x18\x01 \x01(\t\"$\n\x12GetResourceRequest\x12\x0e\n\x06\x63onfig\x18\x01 \x01(\t\"\x1a\n\x08Response\x12\x0e\n\x06result\x18\x01 \x01(\x08\"5\n\x12SubmitTaskResponse\x12\x0e\n\x06result\x18\x01 \x01(\x08\x12\x0f\n\x07task_id\x18\x02 \x01(\t\"P\n\x11GetStatusResponse\x12\x0e\n\x06result\x18\x01 \x01(\x08\x12\x1e\n\x06status\x18\x02 \x01(\x0e\x32\x0e.common.Status\x12\x0b\n\x03log\x18\x03 \x01(\t\"\x80\x02\n\x13GetResourceResponse\x12\x0e\n\x06result\x18\x01 \x01(\x08\x12;\n\x08resource\x18\x02 \x03(\x0b\x32).cluster.GetResourceResponse.ResourcePair\x1a\x9b\x01\n\x0cResourcePair\x12\x44\n\x04type\x18\x01 \x01(\x0e\x32\x36.cluster.GetResourceResponse.ResourcePair.ResourceType\x12\r\n\x05value\x18\x02 \x01(\x03\"6\n\x0cResourceType\x12\x07\n\x03\x43PU\x10\x00\x12\x07\n\x03GPU\x10\x01\x12\x08\n\x04\x44ISK\x10\x02\x12\n\n\x06MEMORY\x10\x03\x32\xfe\x01\n\x0c\x43lusterProxy\x12H\n\x0bGetResource\x12\x1b.cluster.GetResourceRequest\x1a\x1c.cluster.GetResourceResponse\x12\x38\n\nSubmitTask\x12\r.cluster.Task\x1a\x1b.cluster.SubmitTaskResponse\x12/\n\x08KillTask\x12\x10.cluster.Request\x1a\x11.cluster.Response\x12\x39\n\tGetStatus\x12\x10.cluster.Request\x1a\x1a.cluster.GetStatusResponseB\x1d\xaa\x02\x1aMicrosoft.CGC.Cluster.GRPCb\x06proto3')
  ,
  dependencies=[cluster_dot_task__pb2.DESCRIPTOR,status__pb2.DESCRIPTOR,])



_GETRESOURCERESPONSE_RESOURCEPAIR_RESOURCETYPE = _descriptor.EnumDescriptor(
  name='ResourceType',
  full_name='cluster.GetResourceResponse.ResourcePair.ResourceType',
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
  serialized_start=504,
  serialized_end=558,
)
_sym_db.RegisterEnumDescriptor(_GETRESOURCERESPONSE_RESOURCEPAIR_RESOURCETYPE)


_REQUEST = _descriptor.Descriptor(
  name='Request',
  full_name='cluster.Request',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='cluster.Request.id', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=75,
  serialized_end=96,
)


_GETRESOURCEREQUEST = _descriptor.Descriptor(
  name='GetResourceRequest',
  full_name='cluster.GetResourceRequest',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='config', full_name='cluster.GetResourceRequest.config', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=98,
  serialized_end=134,
)


_RESPONSE = _descriptor.Descriptor(
  name='Response',
  full_name='cluster.Response',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='result', full_name='cluster.Response.result', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=136,
  serialized_end=162,
)


_SUBMITTASKRESPONSE = _descriptor.Descriptor(
  name='SubmitTaskResponse',
  full_name='cluster.SubmitTaskResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='result', full_name='cluster.SubmitTaskResponse.result', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='task_id', full_name='cluster.SubmitTaskResponse.task_id', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=164,
  serialized_end=217,
)


_GETSTATUSRESPONSE = _descriptor.Descriptor(
  name='GetStatusResponse',
  full_name='cluster.GetStatusResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='result', full_name='cluster.GetStatusResponse.result', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='status', full_name='cluster.GetStatusResponse.status', index=1,
      number=2, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='log', full_name='cluster.GetStatusResponse.log', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=219,
  serialized_end=299,
)


_GETRESOURCERESPONSE_RESOURCEPAIR = _descriptor.Descriptor(
  name='ResourcePair',
  full_name='cluster.GetResourceResponse.ResourcePair',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='type', full_name='cluster.GetResourceResponse.ResourcePair.type', index=0,
      number=1, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='cluster.GetResourceResponse.ResourcePair.value', index=1,
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
    _GETRESOURCERESPONSE_RESOURCEPAIR_RESOURCETYPE,
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=403,
  serialized_end=558,
)

_GETRESOURCERESPONSE = _descriptor.Descriptor(
  name='GetResourceResponse',
  full_name='cluster.GetResourceResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='result', full_name='cluster.GetResourceResponse.result', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='resource', full_name='cluster.GetResourceResponse.resource', index=1,
      number=2, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[_GETRESOURCERESPONSE_RESOURCEPAIR, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=302,
  serialized_end=558,
)

_GETSTATUSRESPONSE.fields_by_name['status'].enum_type = status__pb2._STATUS
_GETRESOURCERESPONSE_RESOURCEPAIR.fields_by_name['type'].enum_type = _GETRESOURCERESPONSE_RESOURCEPAIR_RESOURCETYPE
_GETRESOURCERESPONSE_RESOURCEPAIR.containing_type = _GETRESOURCERESPONSE
_GETRESOURCERESPONSE_RESOURCEPAIR_RESOURCETYPE.containing_type = _GETRESOURCERESPONSE_RESOURCEPAIR
_GETRESOURCERESPONSE.fields_by_name['resource'].message_type = _GETRESOURCERESPONSE_RESOURCEPAIR
DESCRIPTOR.message_types_by_name['Request'] = _REQUEST
DESCRIPTOR.message_types_by_name['GetResourceRequest'] = _GETRESOURCEREQUEST
DESCRIPTOR.message_types_by_name['Response'] = _RESPONSE
DESCRIPTOR.message_types_by_name['SubmitTaskResponse'] = _SUBMITTASKRESPONSE
DESCRIPTOR.message_types_by_name['GetStatusResponse'] = _GETSTATUSRESPONSE
DESCRIPTOR.message_types_by_name['GetResourceResponse'] = _GETRESOURCERESPONSE
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

Request = _reflection.GeneratedProtocolMessageType('Request', (_message.Message,), dict(
  DESCRIPTOR = _REQUEST,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.Request)
  ))
_sym_db.RegisterMessage(Request)

GetResourceRequest = _reflection.GeneratedProtocolMessageType('GetResourceRequest', (_message.Message,), dict(
  DESCRIPTOR = _GETRESOURCEREQUEST,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.GetResourceRequest)
  ))
_sym_db.RegisterMessage(GetResourceRequest)

Response = _reflection.GeneratedProtocolMessageType('Response', (_message.Message,), dict(
  DESCRIPTOR = _RESPONSE,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.Response)
  ))
_sym_db.RegisterMessage(Response)

SubmitTaskResponse = _reflection.GeneratedProtocolMessageType('SubmitTaskResponse', (_message.Message,), dict(
  DESCRIPTOR = _SUBMITTASKRESPONSE,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.SubmitTaskResponse)
  ))
_sym_db.RegisterMessage(SubmitTaskResponse)

GetStatusResponse = _reflection.GeneratedProtocolMessageType('GetStatusResponse', (_message.Message,), dict(
  DESCRIPTOR = _GETSTATUSRESPONSE,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.GetStatusResponse)
  ))
_sym_db.RegisterMessage(GetStatusResponse)

GetResourceResponse = _reflection.GeneratedProtocolMessageType('GetResourceResponse', (_message.Message,), dict(

  ResourcePair = _reflection.GeneratedProtocolMessageType('ResourcePair', (_message.Message,), dict(
    DESCRIPTOR = _GETRESOURCERESPONSE_RESOURCEPAIR,
    __module__ = 'cluster.clusterservice_pb2'
    # @@protoc_insertion_point(class_scope:cluster.GetResourceResponse.ResourcePair)
    ))
  ,
  DESCRIPTOR = _GETRESOURCERESPONSE,
  __module__ = 'cluster.clusterservice_pb2'
  # @@protoc_insertion_point(class_scope:cluster.GetResourceResponse)
  ))
_sym_db.RegisterMessage(GetResourceResponse)
_sym_db.RegisterMessage(GetResourceResponse.ResourcePair)


DESCRIPTOR.has_options = True
DESCRIPTOR._options = _descriptor._ParseOptions(descriptor_pb2.FileOptions(), _b('\252\002\032Microsoft.CGC.Cluster.GRPC'))

_CLUSTERPROXY = _descriptor.ServiceDescriptor(
  name='ClusterProxy',
  full_name='cluster.ClusterProxy',
  file=DESCRIPTOR,
  index=0,
  options=None,
  serialized_start=561,
  serialized_end=815,
  methods=[
  _descriptor.MethodDescriptor(
    name='GetResource',
    full_name='cluster.ClusterProxy.GetResource',
    index=0,
    containing_service=None,
    input_type=_GETRESOURCEREQUEST,
    output_type=_GETRESOURCERESPONSE,
    options=None,
  ),
  _descriptor.MethodDescriptor(
    name='SubmitTask',
    full_name='cluster.ClusterProxy.SubmitTask',
    index=1,
    containing_service=None,
    input_type=cluster_dot_task__pb2._TASK,
    output_type=_SUBMITTASKRESPONSE,
    options=None,
  ),
  _descriptor.MethodDescriptor(
    name='KillTask',
    full_name='cluster.ClusterProxy.KillTask',
    index=2,
    containing_service=None,
    input_type=_REQUEST,
    output_type=_RESPONSE,
    options=None,
  ),
  _descriptor.MethodDescriptor(
    name='GetStatus',
    full_name='cluster.ClusterProxy.GetStatus',
    index=3,
    containing_service=None,
    input_type=_REQUEST,
    output_type=_GETSTATUSRESPONSE,
    options=None,
  ),
])
_sym_db.RegisterServiceDescriptor(_CLUSTERPROXY)

DESCRIPTOR.services_by_name['ClusterProxy'] = _CLUSTERPROXY

# @@protoc_insertion_point(module_scope)
