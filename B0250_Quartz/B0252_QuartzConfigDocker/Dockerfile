#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["B0252_QuartzConfigDocker/B0252_QuartzConfigDocker.csproj", "B0252_QuartzConfigDocker/"]
RUN dotnet restore "B0252_QuartzConfigDocker/B0252_QuartzConfigDocker.csproj"
COPY . .
WORKDIR "/src/B0252_QuartzConfigDocker"
RUN dotnet build "B0252_QuartzConfigDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "B0252_QuartzConfigDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "B0252_QuartzConfigDocker.dll"]