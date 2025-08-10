# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY NotificationService.API/NotificationService.API.csproj NotificationService.API/
COPY NotificationService.Application/NotificationService.Application.csproj NotificationService.Application/
COPY NotificationService.Domain/NotificationService.Domain.csproj NotificationService.Domain/
COPY NotificationService.Infrastructure/NotificationService.Infrastructure.csproj NotificationService.Infrastructure/
COPY NotificationService.Persistence/NotificationService.Persistence.csproj NotificationService.Persistence/
COPY NotificationService.Contracts/NotificationService.Contracts.csproj NotificationService.Contracts/

RUN dotnet restore NotificationService.API/NotificationService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish NotificationService.API/NotificationService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "NotificationService.API.dll"]