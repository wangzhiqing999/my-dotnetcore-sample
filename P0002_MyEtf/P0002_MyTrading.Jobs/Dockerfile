#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["P0002_MyTrading.Jobs/P0002_MyTrading.Jobs.csproj", "P0002_MyTrading.Jobs/"]
COPY ["MyWebCrawler.Service/MyWebCrawler.Service.csproj", "MyWebCrawler.Service/"]
COPY ["P0002_MyEtf/P0002_MyEtf.csproj", "P0002_MyEtf/"]
COPY ["P0002_MyTrading/P0002_MyTrading.csproj", "P0002_MyTrading/"]
RUN dotnet restore "P0002_MyTrading.Jobs/P0002_MyTrading.Jobs.csproj"
COPY . .
WORKDIR "/src/P0002_MyTrading.Jobs"
RUN dotnet build "P0002_MyTrading.Jobs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P0002_MyTrading.Jobs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P0002_MyTrading.Jobs.dll"]