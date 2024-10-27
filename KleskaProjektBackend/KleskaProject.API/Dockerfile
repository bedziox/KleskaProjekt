#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KleskaProject.API/KleskaProject.API.csproj", "KleskaProject.API/"]
COPY ["KleskaProject.Application/KleskaProject.Application.csproj", "KleskaProject.Application/"]
COPY ["KleskaProject.Domain/KleskaProject.Domain.csproj", "KleskaProject.Domain/"]
COPY ["KleskaProjekt.Infrastructure/KleskaProjekt.Infrastructure.csproj", "KleskaProjekt.Infrastructure/"]
RUN dotnet restore "./KleskaProject.API/KleskaProject.API.csproj"
COPY . .
WORKDIR "/src/KleskaProject.API"
RUN dotnet build "./KleskaProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KleskaProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KleskaProject.API.dll"]