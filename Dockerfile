# ========== BUILD STAGE ==========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy project file first for better layer caching
COPY ["WelshVerbsBlazor.csproj", "."]
RUN dotnet restore "WelshVerbsBlazor.csproj"

# Copy remaining source files
COPY . .
RUN dotnet build "WelshVerbsBlazor.csproj" -c Release -o /app/build

# ========== PUBLISH STAGE ==========
FROM build AS publish
RUN dotnet publish "WelshVerbsBlazor.csproj" -c Release -o /app/publish

# ========== RUNTIME STAGE ==========
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WelshVerbsBlazor.dll"]
