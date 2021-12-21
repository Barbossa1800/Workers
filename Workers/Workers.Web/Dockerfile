#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Workers.Web/Workers.Web.csproj", "Workers.Web/"]
RUN dotnet restore "Workers.Web/Workers.Web.csproj"
COPY . .
WORKDIR "/src/Workers.Web"
RUN dotnet build "Workers.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workers.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workers.Web.dll"]