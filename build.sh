#apk update && apk add openjdk11
#mkdir /usr/share/man/man1/
#apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-11-jre
#dotnet tool install --global dotnet-sonarscanner --version 5.3.1
#dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.12
#dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
#token="8c6d7c89a558c74351bc50187ba9a3f5b0c33396"
#dir="$(pwd)"
#dotnet sonarscanner begin /k:$1 /d:sonar.host.url="http://dev.cicd.geeksbank.co:9000" /d:sonar.login="${token}" /d:sonar.language="cs" /d:sonar.exclusions="**/bin/**/*,**/obj/**/*" /d:sonar.cs.opencover.reportsPaths="${dir}/Tests/**/coverage.opencover.xml"
dotnet build
#dotnet sonarscanner end /d:sonar.login="${token}"