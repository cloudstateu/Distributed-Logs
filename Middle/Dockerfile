#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Middle/Middle.csproj", "Middle/"]
RUN dotnet restore "Middle/Middle.csproj"
COPY . .
WORKDIR "/src/Middle"
RUN dotnet build "Middle.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Middle.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Middle.dll"]
