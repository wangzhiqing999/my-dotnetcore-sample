#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["D0003_Docker_Console_V6/D0003_Docker_Console_V6.csproj", "D0003_Docker_Console_V6/"]
RUN dotnet restore "D0003_Docker_Console_V6/D0003_Docker_Console_V6.csproj"
COPY . .
WORKDIR "/src/D0003_Docker_Console_V6"
RUN dotnet build "D0003_Docker_Console_V6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "D0003_Docker_Console_V6.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D0003_Docker_Console_V6.dll"]