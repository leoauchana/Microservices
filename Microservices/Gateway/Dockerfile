FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ./Src/Api/Gateway.csproj ./Src/Api/

RUN dotnet restore ./Src/Api/Gateway.csproj

COPY . .

WORKDIR /src/Src/Api

RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Gateway.dll" ]