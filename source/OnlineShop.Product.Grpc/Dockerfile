#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["source/OnlineShop.Product.Grpc/OnlineShop.Product.Grpc.csproj", "source/OnlineShop.Product.Grpc/"]
COPY ["shared/OnlineShop.Shared/OnlineShop.Shared.csproj", "shared/OnlineShop.Shared/"]
RUN dotnet restore "./source/OnlineShop.Product.Grpc/./OnlineShop.Product.Grpc.csproj"
COPY . .
WORKDIR "/src/source/OnlineShop.Product.Grpc"
RUN dotnet build "./OnlineShop.Product.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShop.Product.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.Product.Grpc.dll"]