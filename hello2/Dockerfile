# Use .NET 9.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5123
EXPOSE 7037

# Use .NET 9.0 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["hello2.csproj", "./"]
RUN dotnet restore "./hello2.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "hello2.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "hello2.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the final .NET 9.0 runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hello2.dll"]
