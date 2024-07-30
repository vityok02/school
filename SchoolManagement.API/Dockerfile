#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SchoolManagement.API/SchoolManagement.API.csproj", "SchoolManagement.API/"]
COPY ["school.Data/SchoolManagement.Data.csproj", "school.Data/"]
COPY ["school.Models/SchoolManagement.Models.csproj", "school.Models/"]
RUN dotnet restore "./SchoolManagement.API/SchoolManagement.API.csproj"
COPY . .
WORKDIR "/src/SchoolManagement.API"
RUN dotnet build "./SchoolManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SchoolManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolManagement.API.dll"]