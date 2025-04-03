FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ECommerce.ProductApi/ProductApi.Domain/ProductApi.Domain.csproj ./ECommerce.ProductApi/ProductApi.Domain/
COPY ECommerce.ProductApi/ProductApi.Infrastructure/ProductApi.Infrastructure.csproj ./ECommerce.ProductApi/ProductApi.Infrastructure/
COPY ECommerce.ProductApi/ProductApi.Application/ProductApi.Application.csproj ./ECommerce.ProductApi/ProductApi.Application/
COPY ECommerce.ProductApi/ProductApi.Presentation/ProductApi.Presentation.csproj ./ECommerce.ProductApi/ProductApi.Presentation/
COPY SharedLibrary/ECommerce.Shared/ECommerce.Shared.csproj ./SharedLibrary/ECommerce.Shared/
RUN dotnet restore ECommerce.ProductApi/ProductApi.Presentation/ProductApi.Presentation.csproj
COPY ECommerce.ProductApi/ ECommerce.ProductApi/
COPY SharedLibrary/ SharedLibrary/
WORKDIR /src/ECommerce.ProductApi/ProductApi.Presentation
RUN dotnet build ProductApi.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ProductApi.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "ProductApi.Presentation.dll"]