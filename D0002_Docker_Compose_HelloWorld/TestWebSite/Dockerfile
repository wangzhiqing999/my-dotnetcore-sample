#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 8082

ENV ASPNETCORE_URLS=http://+:8082

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TestWebSite/TestWebSite.csproj", "TestWebSite/"]
RUN dotnet restore "TestWebSite/TestWebSite.csproj"
COPY . .
WORKDIR "/src/TestWebSite"
RUN dotnet build "TestWebSite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestWebSite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestWebSite.dll"]