#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["ClientWebApplication/ClientWebApplication.csproj", "ClientWebApplication/"]
RUN dotnet restore "ClientWebApplication/ClientWebApplication.csproj"
COPY . .
WORKDIR "/src/ClientWebApplication"
RUN dotnet build "ClientWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientWebApplication.dll"]
