ARG VERSION=5.0

FROM mcr.microsoft.com/dotnet/sdk:${VERSION} AS build-env

WORKDIR /app

ENV PATH="${PATH}:/root/.dotnet/tools"

ADD . .

RUN chmod 777 build.sh && ./build.sh GeeksBank.Core.Service

RUN dotnet restore *.sln
RUN dotnet publish \
  -c Release \
  -o ./out \
  --no-restore
#----------------
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

ENV APP_HOME=/app
WORKDIR $APP_HOME

RUN adduser --disabled-password --gecos "" app -u 1000
ENV ASPNETCORE_URLS=http://*:5000
ENV COMPlus_EnableDiagnostics=0

ARG ENV
ENV ASPNETCORE_ENVIRONMENT ${ENV:-Production}
COPY --from=build-env --chown=app:app /app/out $APP_HOME
EXPOSE 8080 5000 5001

USER 1000
ENTRYPOINT ["dotnet", "GeeksBank.Core.Api.dll"]