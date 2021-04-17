# PruebaTecnicaBitnovo

## Pasos a seguir 

1. Clonar repositorio
2. Abrir la solución y compilarla
3. Ejecutar el comando update-database en la consola del administrador de paquetes de Nuget. La BBDD se inicializará con los datos del método Seed del archivo Configuration.cs
4. Ejecutar aplicación.
5. La página principal por defecto es la de swagger. Para obtener el token JWT necesario para el resto de llamadas, ir a Login y a "/api/Login" e introducir las credenciales con el formato de ejemplo.
6. Copiar el token del resultado en introducirlo en la barra de la parte superiorq que está a la izquierda del botón "Explore", incluyendo la palabra Bearer delante. Ej: Bearer <token>
