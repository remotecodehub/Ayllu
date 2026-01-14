LABEL org.opencontainers.image.title="Ayllu API"
LABEL org.opencontainers.image.description="WebAPI para organização de movimentos sociais e discussões estruturadas como dialéticas"
LABEL org.opencontainers.image.version="0.0.1"
LABEL org.opencontainers.image.authors="Weslley <weslley@remotecodehub.com>"
LABEL org.opencontainers.image.source="https://github.com/remotecodehub/Ayllu.git"


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY ["src/web/Ayllu.Web/Ayllu.Web.csproj", "src/web/Ayllu.Web/"]
COPY ["src/web/Ayllu.Web.Application/Ayllu.Web.Application.csproj", "src/web/Ayllu.Web.Application/"]
COPY ["src/web/Ayllu.Web.Domain/Ayllu.Web.Domain.csproj", "src/web/Ayllu.Web.Domain/"]
COPY ["src/web/Ayllu.Web.Composition/Ayllu.Web.Composition.csproj", "src/web/Ayllu.Web.Composition/"]
COPY ["src/web/Ayllu.Web.Infrastructure/Ayllu.Web.Infrastructure.csproj", "src/web/Ayllu.Web.Infrastructure/"]
RUN dotnet restore "./src/web/Ayllu.Web/Ayllu.Web.csproj"
COPY . .
WORKDIR "/src/web/Ayllu.Web"
RUN dotnet build "./Ayllu.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ayllu.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENTRYPOINT ["dotnet", "Ayllu.Web.dll"]