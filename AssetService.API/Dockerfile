# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY AssetService.API/AssetService.API.csproj AssetService.API/
COPY AssetService.Application/AssetService.Application.csproj AssetService.Application/
COPY AssetService.Domain/AssetService.Domain.csproj AssetService.Domain/
COPY AssetService.Infrastructure/AssetService.Infrastructure.csproj AssetService.Infrastructure/
COPY AssetService.Persistence/AssetService.Persistence.csproj AssetService.Persistence/
COPY AssetService.Contracts/AssetService.Contracts.csproj AssetService.Contracts/

RUN dotnet restore AssetService.API/AssetService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish AssetService.API/AssetService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AssetService.API.dll"]