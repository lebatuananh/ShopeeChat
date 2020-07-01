#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ShopeeChat.Demo.WebApplication/ShopeeChat.Demo.WebApplication.csproj", "ShopeeChat.Demo.WebApplication/"]
COPY ["ShopeeChat.CoreAPI/ShopeeChat.CoreAPI.csproj", "ShopeeChat.CoreAPI/"]
COPY ["ShopeeChat.Infrastructure/ShopeeChat.Infrastructure.csproj", "ShopeeChat.Infrastructure/"]
RUN dotnet restore "ShopeeChat.Demo.WebApplication/ShopeeChat.Demo.WebApplication.csproj"
COPY . .
WORKDIR "/src/ShopeeChat.Demo.WebApplication"
RUN dotnet build "ShopeeChat.Demo.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopeeChat.Demo.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopeeChat.Demo.WebApplication.dll"]