FROM microsoft/dotnet:latest

ENV http_proxy="http://jophy.samuel:Pass@123456@proxy.phila.gov:8080"
ENV https_proxy="http://jophy.samuel:Pass@123456@proxy.phila.gov:8080"

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]