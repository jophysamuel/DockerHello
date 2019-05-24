FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 25141
EXPOSE 44335

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["DockerHelloSample/DockerHelloSample.csproj", "DockerHelloSample/"]
RUN dotnet restore "DockerHelloSample/DockerHelloSample.csproj"
COPY . .
WORKDIR "/src/DockerHelloSample"
RUN dotnet build "DockerHelloSample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DockerHelloSample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DockerHelloSample.dll"]