# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ClientService.API/ClientService.API.csproj ClientService.API/
COPY ClientService.Application/ClientService.Application.csproj ClientService.Application/
COPY ClientService.Domain/ClientService.Domain.csproj ClientService.Domain/
COPY ClientService.Infrastructure/ClientService.Infrastructure.csproj ClientService.Infrastructure/
COPY ClientService.Persistence/ClientService.Persistence.csproj ClientService.Persistence/
COPY ClientService.Contracts/ClientService.Contracts.csproj ClientService.Contracts/

RUN dotnet restore ClientService.API/ClientService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish ClientService.API/ClientService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ClientService.API.dll"]