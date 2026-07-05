FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/SecretManager.API/SecretManager.API.csproj", "src/SecretManager.API/"]
COPY ["src/SecretManager.Infrastructure/SecretManager.Infrastructure.csproj", "src/SecretManager.Infrastructure/"]
COPY ["src/SecretManager.Application/SecretManager.Application.csproj", "src/SecretManager.Application/"]
COPY ["src/SecretManager.Domain/SecretManager.Domain.csproj", "src/SecretManager.Domain/"]
RUN dotnet restore "src/SecretManager.API/SecretManager.API.csproj"
COPY . .
RUN dotnet publish "src/SecretManager.API/SecretManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SecretManager.API.dll"]