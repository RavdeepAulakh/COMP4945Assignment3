# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
WORKDIR /app
COPY Assignment3/*.csproj ./Assignment3/
RUN dotnet restore ./Assignment3/Assignment3.csproj


# Copy the remaining files and build the application
COPY . .
RUN dotnet build -c Release -o /app/build

# Use the .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .

# Expose port 80 for HTTP traffic
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "YourApp.dll"]
