FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStoreApp.Blazor.Server.UI/BookStoreApp.Blazor.Server.UI.csproj", "BookStoreApp.Blazor.Server.UI/"]
RUN dotnet restore "BookStoreApp.Blazor.Server.UI/BookStoreApp.Blazor.Server.UI.csproj"
COPY . .
WORKDIR "/src/BookStoreApp.Blazor.Server.UI"
RUN dotnet build "BookStoreApp.Blazor.Server.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStoreApp.Blazor.Server.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStoreApp.Blazor.Server.UI.dll"]
