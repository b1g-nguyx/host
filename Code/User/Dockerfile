# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Sửa lại các đường dẫn
COPY ["User.csproj", "./"]
COPY ["../DBAcess/DBAcess.csproj", "../DBAcess/"]
COPY ["../ViewModel/ViewModel.csproj", "../ViewModel/"]

RUN dotnet restore "./User.csproj"

# Copy toàn bộ nội dung
COPY . ./
WORKDIR "/src"
RUN dotnet build "User.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.dll"]
