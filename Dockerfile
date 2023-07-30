FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 57002
EXPOSE 57001
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EShop.Web/EShop.Web.csproj", "EShop.Web/"]
COPY ["EShop.Core/EShop.Core.csproj", "EShop.Core/"]
COPY ["EShop.DataLayer/EShop.DataLayer.csproj", "EShop.DataLayer/"]
RUN dotnet restore "EShop.Web/EShop.Web.csproj"
COPY . .
WORKDIR "/src/EShop.Web"
RUN dotnet build "EShop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Web.dll"]
