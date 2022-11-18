#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SJI3.Worker/SJI3.Worker.csproj", "SJI3.Worker/"]
COPY ["SJI3.Infrastructure/SJI3.Infrastructure.csproj", "SJI3.Infrastructure/"]
COPY ["SJI3.Core/SJI3.Core.csproj", "SJI3.Core/"]
RUN dotnet restore "SJI3.Worker/SJI3.Worker.csproj"
COPY . .
WORKDIR "/src/SJI3.Worker"
RUN dotnet build "SJI3.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SJI3.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SJI3.Worker.dll"]