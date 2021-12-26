#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P0002_MyEtf.SinaReader/P0002_MyEtf.SinaReader.csproj", "P0002_MyEtf.SinaReader/"]
COPY ["P0002_MyEtf/P0002_MyEtf.csproj", "P0002_MyEtf/"]
RUN dotnet restore "P0002_MyEtf.SinaReader/P0002_MyEtf.SinaReader.csproj"
COPY . .
WORKDIR "/src/P0002_MyEtf.SinaReader"
RUN dotnet build "P0002_MyEtf.SinaReader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P0002_MyEtf.SinaReader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P0002_MyEtf.SinaReader.dll"]