#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Consumers/Assessment.EventsConsumer/Assessment.EventsConsumer.csproj", "Consumers/Assessment.EventsConsumer/"]
COPY ["Libs/Assessment.Core/Assessment.Core.csproj", "Libs/Assessment.Core/"]
COPY ["Libs/Assessment.Models/Assessment.Models.csproj", "Libs/Assessment.Models/"]
RUN dotnet restore "Consumers/Assessment.EventsConsumer/Assessment.EventsConsumer.csproj"
COPY . .
WORKDIR "/src/Consumers/Assessment.EventsConsumer"
RUN dotnet build "Assessment.EventsConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assessment.EventsConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assessment.EventsConsumer.dll"]