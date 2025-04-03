FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["CooperativaApp.API/CooperativaApp.API.csproj", "CooperativaApp.API/"]
COPY ["CooperativaApp.Application/CooperativaApp.Application.csproj", "CooperativaApp.Application/"]
COPY ["CooperativaApp.Domain/CooperativaApp.Domain.csproj", "CooperativaApp.Domain/"]
COPY ["CooperativaApp.Infra.Data.SqlServer/CooperativaApp.Infra.Data.SqlServer.csproj", "CooperativaApp.Infra.Data.SqlServer/"]
RUN dotnet restore "./CooperativaApp.API/CooperativaApp.API.csproj"
COPY . .
WORKDIR "/src/CooperativaApp.API"
RUN dotnet build "./CooperativaApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./CooperativaApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CooperativaApp.API.dll", "--environment", "Development"]