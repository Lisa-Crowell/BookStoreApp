FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStoreApp.API/BookStoreApp.API.csproj", "BookStoreApp.API/"]
RUN dotnet restore "BookStoreApp.API/BookStoreApp.API.csproj"
COPY . .
WORKDIR "/src/BookStoreApp.API"
RUN dotnet build "BookStoreApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStoreApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStoreApp.API.dll"]
