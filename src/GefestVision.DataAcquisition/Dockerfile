FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GefestVision.DataAcquisition/GefestVision.DataAcquisition.csproj", "src/GefestVision.DataAcquisition/"]
RUN dotnet restore "src/GefestVision.DataAcquisition/GefestVision.DataAcquisition.csproj"
COPY . .
WORKDIR "/src/src/GefestVision.DataAcquisition"
RUN dotnet build "GefestVision.DataAcquisition.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GefestVision.DataAcquisition.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GefestVision.DataAcquisition.dll"]
