#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P0002_MyTrading.Notice/P0002_MyTrading.Notice.csproj", "P0002_MyTrading.Notice/"]
COPY ["P0002_MyTrading/P0002_MyTrading.csproj", "P0002_MyTrading/"]
COPY ["P0002_MyEtf/P0002_MyEtf.csproj", "P0002_MyEtf/"]
RUN dotnet restore "P0002_MyTrading.Notice/P0002_MyTrading.Notice.csproj"
COPY . .
WORKDIR "/src/P0002_MyTrading.Notice"
RUN dotnet build "P0002_MyTrading.Notice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P0002_MyTrading.Notice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P0002_MyTrading.Notice.dll"]