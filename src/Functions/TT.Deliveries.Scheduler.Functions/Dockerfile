#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Functions/TT.Deliveries.Scheduler.Functions/TT.Deliveries.Scheduler.Functions.csproj", "Functions/TT.Deliveries.Scheduler.Functions/"]
COPY ["Core/TT.Deliveries.Application/TT.Deliveries.Application.csproj", "Core/TT.Deliveries.Application/"]
COPY ["Core/TT.Deliveries.Domain/TT.Deliveries.Domain.csproj", "Core/TT.Deliveries.Domain/"]
RUN dotnet restore "Functions/TT.Deliveries.Scheduler.Functions/TT.Deliveries.Scheduler.Functions.csproj"
COPY . .
WORKDIR "/src/Functions/TT.Deliveries.Scheduler.Functions"
RUN dotnet build "TT.Deliveries.Scheduler.Functions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.Deliveries.Scheduler.Functions.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true