FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


COPY ["TestProduct.csproj", "./"]
RUN dotnet restore "TestProduct.csproj"
COPY . .
RUN dotnet build "TestProduct.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestProduct.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProduct.dll"]