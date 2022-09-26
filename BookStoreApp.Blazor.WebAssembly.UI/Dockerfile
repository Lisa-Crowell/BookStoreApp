FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStoreApp.Blazor.WebAssembly.UI/BookStoreApp.Blazor.WebAssembly.UI.csproj", "BookStoreApp.Blazor.WebAssembly.UI/"]
RUN dotnet restore "BookStoreApp.Blazor.WebAssembly.UI/BookStoreApp.Blazor.WebAssembly.UI.csproj"
COPY . .
WORKDIR "/src/BookStoreApp.Blazor.WebAssembly.UI"
RUN dotnet build "BookStoreApp.Blazor.WebAssembly.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStoreApp.Blazor.WebAssembly.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStoreApp.Blazor.WebAssembly.UI.dll"]
