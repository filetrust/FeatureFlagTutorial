FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
RUN apt update ; apt install wget -y ; rm -rf /var/lib/apt/lists/* ; wget https://github.com/txn2/kubefwd/releases/download/1.4.2/kubefwd_amd64.deb -O kubefwd_amd64.deb ; dpkg -i kubefwd_amd64.deb ; rm -f kubefwd_amd64.deb
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Source/Service/Service.csproj Source/Service/
COPY Source/Initialisation/Initialisation.csproj Source/Initialisation/
COPY Source/Business/Business.csproj Source/Business/
COPY Source/Common/Common.csproj Source/Common/
COPY NuGet.config Packages/
RUN dotnet restore Source/Service/Service.csproj --configfile Packages/NuGet.config
COPY . .
WORKDIR /src/Source/Service
RUN dotnet build Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Glasswall.FileTrust.RepoName.Service.dll"]
