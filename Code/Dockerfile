# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ./DBAcess/DBAcess.csproj ./DBAcess/
COPY ./ViewModel/ViewModel.csproj ./ViewModel/
COPY ./Code/User/User.csproj ./Code/User/

# Restore
WORKDIR /src/Code/User
RUN dotnet restore "User.csproj"

# Copy everything
COPY . .

# Build project
RUN dotnet publish "User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "User.dll"]
