# Use .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY ["./EmployeeMicroserviceAPI.csproj", "EmployeeMicroserviceAPI/"]
WORKDIR "/app/EmployeeMicroserviceAPI"
RUN dotnet restore "EmployeeMicroserviceAPI.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "EmployeeMicroserviceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use lightweight .NET runtime for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy built application
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8082
EXPOSE 8083

# Start application
ENTRYPOINT ["dotnet", "EmployeeMicroserviceAPI.dll"]
