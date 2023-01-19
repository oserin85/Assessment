#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MicroServices/EventServices/Assessment.EventServices.Api/Assessment.EventServices.Api.csproj", "MicroServices/EventServices/Assessment.EventServices.Api/"]
COPY ["Libs/Assessment.Core/Assessment.Core.csproj", "Libs/Assessment.Core/"]
COPY ["Libs/Assessment.Models/Assessment.Models.csproj", "Libs/Assessment.Models/"]
RUN dotnet restore "MicroServices/EventServices/Assessment.EventServices.Api/Assessment.EventServices.Api.csproj"
COPY . .
WORKDIR "/src/MicroServices/EventServices/Assessment.EventServices.Api"
RUN dotnet build "Assessment.EventServices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assessment.EventServices.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assessment.EventServices.Api.dll"]