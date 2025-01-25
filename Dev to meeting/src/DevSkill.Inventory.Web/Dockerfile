FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install nodejs (if needed)
RUN apt update && apt install -y nodejs

# Copy the project files
COPY ["DevSkill.Inventory.Web/*.csproj", "DevSkill.Inventory.Web/"]
COPY ["DevSkill.Inventory.Application/*.csproj", "DevSkill.Inventory.Application/"]
COPY ["DevSkill.Inventory.Domain/*.csproj", "DevSkill.Inventory.Domain/"]
COPY ["DevSkill.Inventory.Infrastructure/*.csproj", "DevSkill.Inventory.Infrastructure/"]

# Restore packages
RUN dotnet restore "DevSkill.Inventory.Web/DevSkill.Inventory.Web.csproj"

# Copy source files, excluding the conflicting appsettings files
COPY . .

# Set working directory and build the project
WORKDIR "/src/DevSkill.Inventory.Web"
RUN dotnet build "DevSkill.Inventory.Web.csproj" -c Release -o /app

# Publish the project to a directory
FROM build AS publish
RUN dotnet publish "DevSkill.Inventory.Web.csproj" -c Release -o /app

# Final image for Web project
FROM build AS final
WORKDIR /app
COPY --from=publish /app .

# Expose port 80 for the web service
EXPOSE 80
ENTRYPOINT ["dotnet", "DevSkill.Inventory.Web.dll"]
