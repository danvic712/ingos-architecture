#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Ingos.Api/Ingos.Api.csproj", "src/Ingos.Api/"]
COPY ["src/03_Application/Ingos.Application/Ingos.Application.csproj", "src/03_Application/Ingos.Application/"]
COPY ["src/01_Infrastructure/Ingos.Infrastructure/Ingos.Infrastructure.csproj", "src/01_Infrastructure/Ingos.Infrastructure/"]
COPY ["src/02_Domain/Ingos.Domain/Ingos.Domain.csproj", "src/02_Domain/Ingos.Domain/"]
RUN dotnet restore "src/Ingos.Api/Ingos.Api.csproj"
COPY . .
WORKDIR "/src/src/Ingos.Api"
RUN dotnet build "Ingos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ingos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ingos.Api.dll"]
