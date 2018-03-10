FROM microsoft/dotnet:2.0.5-sdk-2.1.4-stretch

WORKDIR /src

ADD . /src

RUN dotnet publish ./DataAccess/DataAccess.sln -c Release -o /app

EXPOSE 80

WORKDIR /app

CMD ["dotnet", "Microsoft.CGC.DataAccess.Service.dll"]