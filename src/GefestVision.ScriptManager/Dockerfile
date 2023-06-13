FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GefestVision.ScriptManager/GefestVision.ScriptManager.csproj", "src/GefestVision.ScriptManager/"]
RUN dotnet restore "src/GefestVision.ScriptManager/GefestVision.ScriptManager.csproj"
COPY . .
WORKDIR "/src/src/GefestVision.ScriptManager"
RUN dotnet build "GefestVision.ScriptManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GefestVision.ScriptManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GefestVision.ScriptManager.dll"]
