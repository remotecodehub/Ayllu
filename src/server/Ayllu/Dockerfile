FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER root
RUN apt update && apt install inetutils-ping -y
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/server/Ayllu/Ayllu.csproj", "src/server/Ayllu/"]
COPY ["src/server/Ayllu.Composition/Ayllu.Composition.csproj", "src/server/Ayllu.Composition/"]
COPY ["src/server/Ayllu.Application/Ayllu.Application.csproj", "src/server/Ayllu.Application/"]
COPY ["src/server/Ayllu.Domain/Ayllu.Domain.csproj", "src/server/Ayllu.Domain/"]
COPY ["src/server/Ayllu.Infrastructure/Ayllu.Infrastructure.csproj", "src/server/Ayllu.Infrastructure/"]
RUN dotnet restore "./src/server/Ayllu/Ayllu.csproj"
COPY ./src/ .
WORKDIR "/src/server/Ayllu"
RUN dotnet build "./Ayllu.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ayllu.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ayllu.dll"]