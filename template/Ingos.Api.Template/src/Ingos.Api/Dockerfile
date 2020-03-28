#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Ingos.Api/Ingos.Api.csproj", "src/Ingos.Api/"]
COPY ["src/01_Infrastructure/Shared/Ingos.Api.Core/Ingos.Api.Core.csproj", "src/01_Infrastructure/Shared/Ingos.Api.Core/"]
COPY ["src/01_Infrastructure/Shared/Ingos.Infrastructure.Core/Ingos.Infrastructure.Core.csproj", "src/01_Infrastructure/Shared/Ingos.Infrastructure.Core/"]
COPY ["src/01_Infrastructure/Shared/Ingos.Domain.Abstractions/Ingos.Domain.Abstractions.csproj", "src/01_Infrastructure/Shared/Ingos.Domain.Abstractions/"]
COPY ["src/03_Application/Ingos.Application/Ingos.Application.csproj", "src/03_Application/Ingos.Application/"]
COPY ["src/02_Domain/Ingos.Domain/Ingos.Domain.csproj", "src/02_Domain/Ingos.Domain/"]
COPY ["src/01_Infrastructure/Ingos.Infrastructure/Ingos.Infrastructure.csproj", "src/01_Infrastructure/Ingos.Infrastructure/"]
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