FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestApi.csproj", "TestApi/"]
RUN dotnet restore "./TestApi/TestApi.csproj"
WORKDIR "/src/TestApi"
COPY . .
RUN dotnet build "./TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./TestApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApi.dll"]