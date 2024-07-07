FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LetHimCook.API/LetHimCook.API.csproj", "src/LetHimCook.API/"]
RUN dotnet restore "src/LetHimCook.API/LetHimCook.API.csproj"
COPY . .
WORKDIR "/src/src/LetHimCook.API"
RUN dotnet build "LetHimCook.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LetHimCook.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LetHimCook.API.dll"]
