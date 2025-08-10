# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY InspectionService.API/InspectionService.API.csproj InspectionService.API/
COPY InspectionService.Application/InspectionService.Application.csproj InspectionService.Application/
COPY InspectionService.Domain/InspectionService.Domain.csproj InspectionService.Domain/
COPY InspectionService.Infrastructure/InspectionService.Infrastructure.csproj InspectionService.Infrastructure/
COPY InspectionService.Persistence/InspectionService.Persistence.csproj InspectionService.Persistence/
COPY InspectionService.Contracts/InspectionService.Contracts.csproj InspectionService.Contracts/

RUN dotnet restore InspectionService.API/InspectionService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish InspectionService.API/InspectionService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "InspectionService.API.dll"]