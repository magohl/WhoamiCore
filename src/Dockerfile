FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Install network tools
RUN apk add --no-cache \
    bind-tools \
    curl \
    iputils

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["WhoamiCore.csproj", "./"]
RUN dotnet restore "./WhoamiCore.csproj"
COPY . .
WORKDIR /src/.
RUN dotnet build "WhoamiCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhoamiCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "WhoamiCore.dll"]