# Sección de construcción del contenedor

# Se obtiene una imagén base de .NET 8 que facilita la construcción de una aplicación .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build


# Se declara variable disponible solo en el build
ARG BUILD_CONFIGURATION=Release

# Se crea la carpeta principal dentro del contenedor
WORKDIR /src

# Se copian los archivos .csproj en la carpeta principal del contenedor
COPY ./Src/Api/Api.csproj ./Src/Api/
COPY ./Src/Application/Application.csproj ./Src/Application/
COPY ./Src/Domain/Domain.csproj ./Src/Domain/
COPY ./Src/Infraestructure/Infraestructure.csproj ./Src/Infraestructure/

# Se restauran todos los paquetes nuget
RUN dotnet restore ./Src/Api/Api.csproj

# Se copia el resto del codigo a la carpeta principal del contendor
COPY . .

# Esta separación se realiza para aprovechar la cache que mantiene docker de las capas
# y que no se ejecute el dotnet restore cuando se modifique una clase cs
# si no cuando cambia las dependencias de algún archivo .csproj

# Se cambia al directorio donde se encuentra el proyecto de inicio para la ejecución
# del comando de compilación y generación de binarios
WORKDIR /src/Src/Api

# Se compila la solución y se genera los binarios y se los almacenan en la carpeta publish
# dentro de la carpeta donde se encontrara la aplicación ejecutando
RUN dotnet publish -c ${BUILD_CONFIGURATION} -o /app/publish --no-restore
# La bandera --no-restore indica que no se ejecute o restaure los paquetes nuget

# Sección de ejecución

# Obtenemos una imágen base para la ejecución de nuestra aplicacion la cual es más liviana
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Se crea el directorio en el contenedor donde se alojara mi aplicación en ejecución
WORKDIR /app

# Indica el puerto el cual expone el contenedor
EXPOSE 5002

# Se copia todos los archivos generados en la etapa de build en el directorio de app
COPY --from=build /app/publish .

# Se define el comando que se ejecutara al iniciar el contenedor
ENTRYPOINT [ "dotnet", "Api.dll" ]
