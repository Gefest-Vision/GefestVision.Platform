FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GefestVision.Preprocessing/GefestVision.Preprocessing.csproj", "src/GefestVision.Preprocessing/"]
RUN dotnet restore "src/GefestVision.Preprocessing/GefestVision.Preprocessing.csproj"
COPY . .
WORKDIR "/src/src/GefestVision.Preprocessing"
RUN dotnet build "GefestVision.Preprocessing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GefestVision.Preprocessing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GefestVision.Preprocessing.dll"]
