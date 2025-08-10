# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ReportService.API/ReportService.API.csproj ReportService.API/
COPY ReportService.Application/ReportService.Application.csproj ReportService.Application/
COPY ReportService.Domain/ReportService.Domain.csproj ReportService.Domain/
COPY ReportService.Infrastructure/ReportService.Infrastructure.csproj ReportService.Infrastructure/
COPY ReportService.Persistence/ReportService.Persistence.csproj ReportService.Persistence/
COPY ReportService.Contracts/ReportService.Contracts.csproj ReportService.Contracts/

RUN dotnet restore ReportService.API/ReportService.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish ReportService.API/ReportService.API.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ReportService.API.dll"]