FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.10 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.10 AS build
WORKDIR /src
COPY . .
RUN dotnet restore GefestVision.APIs.Management/GefestVision.APIs.Management.csproj
COPY . .
WORKDIR "/src/GefestVision.APIs.Management"
RUN dotnet build GefestVision.APIs.Management.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish GefestVision.APIs.Management.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GefestVision.APIs.Management.dll"]