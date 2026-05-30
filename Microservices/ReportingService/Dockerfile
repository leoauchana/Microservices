FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY /Src/Api/Api.csproj /Src/Api
COPY /Src/Application/Application.csproj /Src/Application
COPY /Src/Infraestructure/Infraestructure.csproj /Src/Infraestructure

RUN dotnet restore /Src/Api/Api.csproj

COPY . .

WORKDIR /src/Src/Api

RUN dotnet publish ${BUILD_CONFIGURATION} -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/asp:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5004

ENTRYPOINT [ "dotnet", "Api.dll" ]