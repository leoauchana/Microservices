FROM msr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ./Src/Api/Api.csproj ./Src/Api
COPY ./Src/Application/Application.csproj ./Src/Application
COPY ./Src/Domain/Domain.csproj ./Src/Domain
COPY ./Src/Infraestructure/Infraestructure.csproj ./Src/Infraestructure

RUN dotnet restore ./Src/Api/Api.csproj

COPY . .

WORKDIR /src/Src/Api

RUN dotnet publish -c Release -o /app/publish --no-restore

FROM msr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Api.dll" ]