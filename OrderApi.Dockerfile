FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ECommerce.OrderApi/OrderApi.Domain/OrderApi.Domain.csproj ./ECommerce.OrderApi/OrderApi.Domain/
COPY ECommerce.OrderApi/OrderApi.Infrastructure/OrderApi.Infrastructure.csproj ./ECommerce.OrderApi/OrderApi.Infrastructure/
COPY ECommerce.OrderApi/OrderApi.Application/OrderApi.Application.csproj ./ECommerce.OrderApi/OrderApi.Application/
COPY ECommerce.OrderApi/OrderApi.Presentation/OrderApi.Presentation.csproj ./ECommerce.OrderApi/OrderApi.Presentation/
COPY SharedLibrary/ECommerce.Shared/ECommerce.Shared.csproj ./SharedLibrary/ECommerce.Shared/
RUN dotnet restore ECommerce.OrderApi/OrderApi.Presentation/OrderApi.Presentation.csproj
COPY ECommerce.OrderApi/ ECommerce.OrderApi/
COPY SharedLibrary/ SharedLibrary/
WORKDIR /src/ECommerce.OrderApi/OrderApi.Presentation
RUN dotnet build OrderApi.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish OrderApi.Presentation.csproj -c $BUILD_CONFIGURATION -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "OrderApi.Presentation.dll"]