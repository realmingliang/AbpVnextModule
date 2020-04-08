#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Tudou.Grace.HttpApi.Host/Tudou.Grace.HttpApi.Host.csproj", "src/Tudou.Grace.HttpApi.Host/"]
COPY ["src/Tudou.Grace.Application/Tudou.Grace.Application.csproj", "src/Tudou.Grace.Application/"]
COPY ["src/Tudou.Grace.Domain/Tudou.Grace.Domain.csproj", "src/Tudou.Grace.Domain/"]
COPY ["src/Tudou.Grace.Domain.Shared/Tudou.Grace.Domain.Shared.csproj", "src/Tudou.Grace.Domain.Shared/"]
COPY ["src/Tudou.Grace.Application.Contracts/Tudou.Grace.Application.Contracts.csproj", "src/Tudou.Grace.Application.Contracts/"]
COPY ["src/Tudou.Grace.EntityFrameworkCore.DbMigrations/Tudou.Grace.EntityFrameworkCore.DbMigrations.csproj", "src/Tudou.Grace.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Tudou.Grace.EntityFrameworkCore/Tudou.Grace.EntityFrameworkCore.csproj", "src/Tudou.Grace.EntityFrameworkCore/"]
COPY ["src/Tudou.Grace.HttpApi/Tudou.Grace.HttpApi.csproj", "src/Tudou.Grace.HttpApi/"]
RUN dotnet restore "src/Tudou.Grace.HttpApi.Host/Tudou.Grace.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Tudou.Grace.HttpApi.Host"
RUN dotnet build "Tudou.Grace.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tudou.Grace.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tudou.Grace.HttpApi.Host.dll"]