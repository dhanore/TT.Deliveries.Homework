#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/TT.Deliveries.Web.Api/TT.Deliveries.Web.Api.csproj", "Web/TT.Deliveries.Web.Api/"]
COPY ["Core/TT.Deliveries.Application/TT.Deliveries.Application.csproj", "Core/TT.Deliveries.Application/"]
COPY ["Core/TT.Deliveries.Domain/TT.Deliveries.Domain.csproj", "Core/TT.Deliveries.Domain/"]
COPY ["Infrastructure/TT.Deliveries.Infrastructure/TT.Deliveries.Persistence.csproj", "Infrastructure/TT.Deliveries.Infrastructure/"]
RUN dotnet restore "Web/TT.Deliveries.Web.Api/TT.Deliveries.Web.Api.csproj"
COPY . .
WORKDIR "/src/Web/TT.Deliveries.Web.Api"
RUN dotnet build "TT.Deliveries.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.Deliveries.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.Deliveries.Web.Api.dll"]