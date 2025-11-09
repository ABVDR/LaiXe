# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN mkdir -p /app/wwwroot/images /app/wwwroot/videos && \
    chown -R $APP_UID:$APP_UID /app/wwwroot
USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ET/ET.csproj", "ET/"]
COPY ["Libs/Libs.csproj", "Libs/"]
RUN dotnet restore "./ET/ET.csproj"
COPY . .
WORKDIR "/src/ET"
RUN dotnet build "./ET.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ET.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN mkdir -p /app/wwwroot/images /app/wwwroot/videos && \
    chown -R $APP_UID:$APP_UID /app/wwwroot
USER $APP_UID
ENTRYPOINT ["dotnet", "ET.dll"]