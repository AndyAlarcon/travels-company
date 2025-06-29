# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Clientes/*.sln .
COPY Clientes/Clientes.API/*.csproj ./Clientes.API/
COPY Clientes/Clientes.Application/*.csproj ./Clientes.Application/
COPY Clientes/Clientes.Domain/*.csproj ./Clientes.Domain/
COPY Clientes/Clientes.Infrastructure/*.csproj ./Clientes.Infrastructure/

RUN dotnet restore

COPY . .
RUN dotnet publish Clientes/Clientes.API -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Clientes.API.dll"]
