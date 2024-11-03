#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "CartApi.csproj"
RUN dotnet build "CartApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Copy the certificate
#COPY my-cert.crt /etc/ssl/certs/
#RUN update-ca-certificates

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CartApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CartApi.dll"]