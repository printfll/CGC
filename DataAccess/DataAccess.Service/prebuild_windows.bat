dir
SET ProjectDir=%cd%\
SET PROTODIR=%ProjectDir%..\..\proto\dataaccess\
SET PROTOC=%PROTODIR%\windows_x64\protoc.exe
SET PROTOCGRPC=%PROTODIR%\windows_x64\grpc_csharp_plugin.exe

for %%f in (%PROTODIR%*.proto) do echo %%f && %PROTOC% -I=%PROTODIR% --csharp_out=%ProjectDir%Service --grpc_out=%ProjectDir%Service %%f --plugin=protoc-gen-grpc=%PROTOCGRPC%
REM for /f %%f in (dir /b c:\`) do echo %%f