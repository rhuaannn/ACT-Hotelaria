FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACT-Hotelaria/ACT-Hotelaria.csproj", "ACT-Hotelaria/"]
COPY ["ACT-Hotelaria.Application/ACT-Hotelaria.Application.csproj", "ACT-Hotelaria.Application/"]
COPY ["ACT-Hotelaria.Domain/ACT-Hotelaria.Domain.csproj", "ACT-Hotelaria.Domain/"]
COPY ["ACT-Hotelaria.Message/ACT-Hotelaria.Message.csproj", "ACT-Hotelaria.Message/"]
COPY ["ACT-Hotelaria.Redis/ACT-Hotelaria.Redis.csproj", "ACT-Hotelaria.Redis/"]
COPY ["ACT-Hotelaria.SqlServer/ACT-Hotelaria.SqlServer.csproj", "ACT-Hotelaria.SqlServer/"]
COPY ["ACT-Hotelaria.Auth/ACT-Hotelaria.Auth.csproj", "ACT-Hotelaria.Auth/"]
RUN dotnet restore "ACT-Hotelaria/ACT-Hotelaria.csproj"
COPY . .
WORKDIR "/src/ACT-Hotelaria"
RUN dotnet build "./ACT-Hotelaria.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ACT-Hotelaria.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACT-Hotelaria.dll"]
