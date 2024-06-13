# Hacker News API - ASP.NET Core

Este proyecto implementa una API RESTful en ASP.NET Core para obtener detalles de las mejores historias de Hacker News según su puntaje.

## Descripción

La API utiliza la [Hacker News API](https://github.com/HackerNews/API) para obtener los IDs de las mejores historias y luego obtiene los detalles de cada historia individualmente. Los detalles incluyen el título, la URL, el autor, la fecha de publicación, el puntaje y el número de comentarios de cada historia.

## Tecnologías Utilizadas

- ASP.NET Core 3.1
- HttpClient para realizar solicitudes HTTP
- System.Text.Json para deserialización JSON
- IMemoryCache para cachear los IDs de historias
- Swagger para documentación de la API

## Configuración del Proyecto

### Requisitos Previos

- .NET 5 SDK o superior
- Visual Studio 2019 o Visual Studio Code (opcional)

### Ejecución del Proyecto

1. Clona el repositorio desde GitHub:

   ```bash
   git clone https://github.com/tu-usuario/hacker-news-api.git
```
2. Abre el proyecto en tu entorno de desarrollo (Visual Studio, VS Code, etc.).

3. Configura las URLs base y otras configuraciones en el archivo appsettings.json.

4. Compila y ejecuta la aplicación. Puedes usar el comando dotnet run desde la línea de comandos o ejecutar desde tu IDE.

5. Una vez ejecutándose, puedes acceder a la API desde http://localhost:5000 (o el puerto que se especifique en tu configuración).

Endpoints Disponibles
```
GET /api/hackernews/beststories?n={number}: Retorna los detalles de las mejores n historias según su puntaje.
```

### Ejemplo de Uso
Para obtener las mejores 10 historias, realiza una solicitud GET a [http://localhost:5000/api/hackernews/beststories?n=10](http://localhost:5198/HackerNews/beststories?n=10):

http
```
GET http://localhost:5000/api/hackernews/beststories?n=10
```
Respuesta esperada:

```
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  },
  {
    "title": "Another Tesla on Autopilot crashes into a stationary object; 3 dead",
    "uri": "https://www.tesla.com/autopilot",
    "postedBy": "john_doe",
    "time": "2020-05-20T09:15:00+00:00",
    "score": 1452,
    "commentCount": 318
  },
  ...
]
```

### Notas Adicionales
La aplicación está configurada para cacheo de los IDs de historias por 5 minutos para mejorar el rendimiento y reducir el número de solicitudes a la API de Hacker News.
Puedes ajustar la configuración de la cache o las URLs base según sea necesario en el archivo appsettings.json.
