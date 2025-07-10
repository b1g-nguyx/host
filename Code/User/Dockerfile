# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files từ thư mục gốc repo
COPY ../DBAcess/DBAcess.csproj ../DBAcess/
COPY ../ViewModel/ViewModel.csproj ../ViewModel/
COPY User.csproj ./

# Restore dependencies
RUN dotnet restore ./User.csproj

# Copy toàn bộ source code
COPY . ./
COPY ../DBAcess ../DBAcess
COPY ../ViewModel ../ViewModel

# Build
RUN dotnet publish "User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "User.dll"]
