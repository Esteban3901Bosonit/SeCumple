# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar la solución y todos los archivos del proyecto de una vez
COPY . ./

# Restaurar dependencias
RUN dotnet restore

# Publicar el proyecto principal (SeCumple.API)
RUN dotnet publish SeCumple.API/SeCumple.API.csproj -c Release -o out

# Etapa de producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exponer el puerto (ajustar si se usa otro puerto)
EXPOSE 8080
EXPOSE 443

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "SeCumple.API.dll"]
