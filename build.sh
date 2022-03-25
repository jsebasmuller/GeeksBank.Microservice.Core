#apk update && apk add openjdk11
mkdir /usr/share/man/man1/
apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-11-jre
dotnet tool install --global dotnet-sonarscanner --version 5.3.1
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.12
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
token="22d3ea8eed9ec3d3f656df90465affafbd289dd2"
dir="$(pwd)"
dotnet sonarscanner begin /k:$1 /d:sonar.host.url="http://40.65.245.142:9000" /d:sonar.login="${token}" /d:sonar.language="cs" /d:sonar.exclusions="**/bin/**/*,**/obj/**/*" /d:sonar.cs.opencover.reportsPaths="${dir}/Tests/**/coverage.opencover.xml"
dotnet build
#dotnet sonarscanner end /d:sonar.login="${token}"