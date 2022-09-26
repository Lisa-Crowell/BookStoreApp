FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStoreApp.WebAssembly.UI/BookStoreApp.WebAssembly.UI.csproj", "BookStoreApp.WebAssembly.UI/"]
RUN dotnet restore "BookStoreApp.WebAssembly.UI/BookStoreApp.WebAssembly.UI.csproj"
COPY . .
WORKDIR "/src/BookStoreApp.WebAssembly.UI"
RUN dotnet build "BookStoreApp.WebAssembly.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStoreApp.WebAssembly.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStoreApp.WebAssembly.UI.dll"]
