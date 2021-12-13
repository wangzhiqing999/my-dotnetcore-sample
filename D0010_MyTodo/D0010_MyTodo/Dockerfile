#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["D0010_MyTodo/D0010_MyTodo.csproj", "D0010_MyTodo/"]
RUN dotnet restore "D0010_MyTodo/D0010_MyTodo.csproj"
COPY . .
WORKDIR "/src/D0010_MyTodo"
RUN dotnet build "D0010_MyTodo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "D0010_MyTodo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D0010_MyTodo.dll"]