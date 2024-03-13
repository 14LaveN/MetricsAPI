FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MetricsAPI/MetricsAPI.csproj", "MetricsAPI/"]
RUN dotnet restore "MetricsAPI/MetricsAPI.csproj"
COPY . .
WORKDIR "/src/MetricsAPI"
RUN dotnet build "MetricsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MetricsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MetricsAPI.dll"]
