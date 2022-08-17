#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Jfi.Matomo.Demo/Jfi.Matomo.Demo.csproj", "Jfi.Matomo.Demo/"]
RUN dotnet restore "Jfi.Matomo.Demo/Jfi.Matomo.Demo.csproj"
COPY . .
WORKDIR "/src/Jfi.Matomo.Demo"
RUN dotnet build "Jfi.Matomo.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jfi.Matomo.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jfi.Matomo.Demo.dll"]