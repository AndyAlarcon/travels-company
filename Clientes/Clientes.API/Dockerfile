# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY Clientes.API/*.csproj ./Clientes.API/
COPY Clientes.Application/*.csproj ./Clientes.Application/
COPY Clientes.Domain/*.csproj ./Clientes.Domain/
COPY Clientes.Infrastructure/*.csproj ./Clientes.Infrastructure/

RUN dotnet restore

COPY . .
RUN dotnet publish Clientes.API -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Clientes.API.dll"]
