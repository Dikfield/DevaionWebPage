# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia o arquivo .csproj específico e restaura as dependências
COPY . .
RUN dotnet restore "./API/API.csproj" --disable-parallel

RUN dotnet publish -c Release -0 ./publish

# Etapa de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "API.dll"]