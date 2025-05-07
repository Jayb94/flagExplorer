# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /app

# This stage is used to build the service project
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlagExplorer.API/FlagExplorer.API.csproj", "FlagExplorer.API/"]
COPY ["FlagExplorer.Domain/FlagExplorer.Domain.csproj", "FlagExplorer.Domain/"]
COPY ["FlagExplorer.Infrastructure/FlagExplorer.Infrastructure.csproj", "FlagExplorer.Infrastructure/"]
COPY ["FlagExplorer.Tests/FlagExplorer.Tests.csproj", "FlagExplorer.Tests/"]
RUN dotnet restore "FlagExplorer.API/FlagExplorer.API.csproj"

COPY FlagExplorer.API/. FlagExplorer.API/
COPY FlagExplorer.Domain/. FlagExplorer.Domain/
COPY FlagExplorer.Infrastructure/. FlagExplorer.Infrastructure/
COPY FlagExplorer.Tests/. FlagExplorer.Tests/

WORKDIR "/src/FlagExplorer.Tests"
RUN dotnet build FlagExplorer.Tests.csproj -c Release -o /app/test

WORKDIR "/src/FlagExplorer.API"
RUN dotnet build "FlagExplorer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS ci-test
WORKDIR /app/test
RUN dotnet test FlagExplorer.Tests.dll --logger:trx

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FlagExplorer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS final
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "FlagExplorer.API.dll"]