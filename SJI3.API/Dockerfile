#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SJI3.API/SJI3.API.csproj", "SJI3.API/"]
COPY ["SJI3.Core/SJI3.Core.csproj", "SJI3.Core/"]
COPY ["SJI3.Infrastructure/SJI3.Infrastructure.csproj", "SJI3.Infrastructure/"]
RUN dotnet restore "SJI3.API/SJI3.API.csproj"
COPY . .
WORKDIR "/src/SJI3.API"
RUN dotnet build "SJI3.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SJI3.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SJI3.API.dll"]