#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KleskaProjekt.API/KleskaProjekt.API.csproj", "KleskaProjekt.API/"]
COPY ["KleskaProject.Domain/KleskaProject.Domain.csproj", "KleskaProject.Domain/"]
RUN dotnet restore "./KleskaProjekt.API/KleskaProjekt.API.csproj"
COPY . .
WORKDIR "/src/KleskaProjekt.API"
RUN dotnet build "./KleskaProjekt.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KleskaProjekt.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KleskaProjekt.API.dll"]