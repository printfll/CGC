# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: jobmanager/parameter.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()


from jobmanager import value_type_pb2 as jobmanager_dot_value__type__pb2


DESCRIPTOR = _descriptor.FileDescriptor(
  name='jobmanager/parameter.proto',
  package='grpc.jobmanager',
  syntax='proto3',
  serialized_pb=_b('\n\x1ajobmanager/parameter.proto\x12\x0fgrpc.jobmanager\x1a\x1bjobmanager/value_type.proto\"]\n\x0eInputParameter\x12\x0c\n\x04name\x18\x01 \x01(\t\x12.\n\nvalue_type\x18\x02 \x01(\x0e\x32\x1a.grpc.jobmanager.ValueType\x12\r\n\x05value\x18\x03 \x01(\t\"d\n\x0fOutputParameter\x12\x0c\n\x04name\x18\x01 \x01(\t\x12.\n\nvalue_type\x18\x02 \x01(\x0e\x32\x1a.grpc.jobmanager.ValueType\x12\x13\n\x0boutput_path\x18\x03 \x01(\tB \xaa\x02\x1dMicrosoft.CGC.GRPC.JobManagerb\x06proto3')
  ,
  dependencies=[jobmanager_dot_value__type__pb2.DESCRIPTOR,])




_INPUTPARAMETER = _descriptor.Descriptor(
  name='InputParameter',
  full_name='grpc.jobmanager.InputParameter',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='name', full_name='grpc.jobmanager.InputParameter.name', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value_type', full_name='grpc.jobmanager.InputParameter.value_type', index=1,
      number=2, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='grpc.jobmanager.InputParameter.value', index=2,
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
  serialized_start=76,
  serialized_end=169,
)


_OUTPUTPARAMETER = _descriptor.Descriptor(
  name='OutputParameter',
  full_name='grpc.jobmanager.OutputParameter',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='name', full_name='grpc.jobmanager.OutputParameter.name', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value_type', full_name='grpc.jobmanager.OutputParameter.value_type', index=1,
      number=2, type=14, cpp_type=8, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='output_path', full_name='grpc.jobmanager.OutputParameter.output_path', index=2,
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
  serialized_start=171,
  serialized_end=271,
)

_INPUTPARAMETER.fields_by_name['value_type'].enum_type = jobmanager_dot_value__type__pb2._VALUETYPE
_OUTPUTPARAMETER.fields_by_name['value_type'].enum_type = jobmanager_dot_value__type__pb2._VALUETYPE
DESCRIPTOR.message_types_by_name['InputParameter'] = _INPUTPARAMETER
DESCRIPTOR.message_types_by_name['OutputParameter'] = _OUTPUTPARAMETER
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

InputParameter = _reflection.GeneratedProtocolMessageType('InputParameter', (_message.Message,), dict(
  DESCRIPTOR = _INPUTPARAMETER,
  __module__ = 'jobmanager.parameter_pb2'
  # @@protoc_insertion_point(class_scope:grpc.jobmanager.InputParameter)
  ))
_sym_db.RegisterMessage(InputParameter)

OutputParameter = _reflection.GeneratedProtocolMessageType('OutputParameter', (_message.Message,), dict(
  DESCRIPTOR = _OUTPUTPARAMETER,
  __module__ = 'jobmanager.parameter_pb2'
  # @@protoc_insertion_point(class_scope:grpc.jobmanager.OutputParameter)
  ))
_sym_db.RegisterMessage(OutputParameter)


DESCRIPTOR.has_options = True
DESCRIPTOR._options = _descriptor._ParseOptions(descriptor_pb2.FileOptions(), _b('\252\002\035Microsoft.CGC.GRPC.JobManager'))
# @@protoc_insertion_point(module_scope)