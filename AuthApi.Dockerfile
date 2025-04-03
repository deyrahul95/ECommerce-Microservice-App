FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ECommerce.AuthApi/AuthApi/AuthApi.csproj ECommerce.AuthApi/AuthApi/
COPY SharedLibrary/ECommerce.Shared/ECommerce.Shared.csproj SharedLibrary/ECommerce.Shared/
RUN dotnet restore ECommerce.AuthApi/AuthApi/AuthApi.csproj
COPY ECommerce.AuthApi/ ECommerce.AuthApi/
COPY SharedLibrary/ SharedLibrary/
WORKDIR /src/ECommerce.AuthApi/AuthApi
RUN dotnet build AuthApi.csproj -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish AuthApi.csproj -c $BUILD_CONFIGURATION -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "AuthApi.dll"]