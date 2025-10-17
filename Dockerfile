# Use the official ASP.NET runtime as the base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["ProfileApi.csproj", "./"]
RUN dotnet restore "ProfileApi.csproj"

# Copy the rest of the source code and build the project
COPY . .
RUN dotnet publish "ProfileApi.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ProfileApi.dll"]