# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY AuthService.API/AuthService.API.csproj AuthService.API/
COPY AuthService.Application/AuthService.Application.csproj AuthService.Application/
COPY AuthService.Domain/AuthService.Domain.csproj AuthService.Domain/
COPY AuthService.Infrastructure/AuthService.Infrastructure.csproj AuthService.Infrastructure/
COPY AuthService.Persistence/AuthService.Persistence.csproj AuthService.Persistence/
COPY AuthService.Contracts/AuthService.Contracts.csproj AuthService.Contracts/

RUN dotnet restore AuthService.API/AuthService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish AuthService.API/AuthService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]