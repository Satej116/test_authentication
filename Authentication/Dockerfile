# Use .NET 9.0 runtime for final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use .NET 9.0 SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Authentication.csproj Authentication/
WORKDIR /src/Authentication
RUN dotnet restore "Authentication.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/Authentication"
RUN dotnet build "Authentication.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Authentication.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the final .NET 9.0 runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.dll"]
