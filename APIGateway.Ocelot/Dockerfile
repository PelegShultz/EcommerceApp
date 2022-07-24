#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIGateway.Ocelot/APIGateway.Ocelot.csproj", "APIGateway.Ocelot/"]
RUN dotnet restore "APIGateway.Ocelot/APIGateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/APIGateway.Ocelot"
RUN dotnet build "APIGateway.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGateway.Ocelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.Ocelot.dll"]