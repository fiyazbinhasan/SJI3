#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SJI3.Identity/SJI3.Identity.csproj", "SJI3.Identity/"]
RUN dotnet restore "SJI3.Identity/SJI3.Identity.csproj"
COPY . .
WORKDIR "/src/SJI3.Identity"
RUN dotnet build "SJI3.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SJI3.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SJI3.Identity.dll"]