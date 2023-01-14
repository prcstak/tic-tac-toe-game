FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backgroundService/backgroundService.csproj", "backgroundService/"]
COPY ["common/common.csproj", "common/"]
COPY ["infrastructure/infrastructure.csproj", "infrastructure/"]
COPY ["domain/domain.csproj", "domain/"]
COPY ["interfaces/interfaces.csproj", "interfaces/"]
COPY ["application/application.csproj", "application/"]
RUN dotnet restore "backgroundService/backgroundService.csproj"
COPY . .
WORKDIR "/src/backgroundService"
RUN dotnet build "backgroundService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backgroundService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backgroundService.dll"]
