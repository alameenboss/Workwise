#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core/Workwise.API/Workwise.API.csproj", "Core/Workwise.API/"]
RUN dotnet restore "Core/Workwise.API/Workwise.API.csproj"
COPY . .
WORKDIR "/src/Core/Workwise.API"
RUN dotnet build "Workwise.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workwise.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workwise.API.dll"]