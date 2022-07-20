#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core.Main/Core.Main.csproj", "Core.Main/"]
RUN dotnet restore "Core.Main/Core.Main.csproj"
COPY . .
WORKDIR "/src/Core.Main"
RUN dotnet build "Core.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Main.dll"]