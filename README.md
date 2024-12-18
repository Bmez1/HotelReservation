# HotelReservation
El proyecto "HotelReservation" es una aplicación diseñada para gestionar reservas de hoteles. Incluye funcionalidades para manejar hoteles, habitaciones, reservas y pasajeros. La aplicación está estructurada en varias capas, 
incluyendo la capa de dominio, aplicación e infraestructura.

El proyecto aplica Arquitectura limpia y sigue principios de Domain-Driven Design, con una clara separación de las capas de dominio, aplicación e infraestructura, y el uso de patrones comunes como entidades, repositorios y objetos de valor.

## Patrones de Diseño
- Repositorio (Repository Pattern)
- Inyección de Dependencias (Dependency Injection)
- CQRS (Command Query Responsibility Segregation)
- SOLID

## Estructura del Proyecto
- HotelReservation.Domain: Contiene las entidades principales como Hotel, Room, Reservation, y sus respectivos atributos y métodos.
- HotelReservation.Application: Incluye los casos de uso y DTOs (Data Transfer Objects) para manejar la lógica de negocio y la comunicación entre las capas.
- HotelReservation.Infraestructure: Proporciona la implementación de los repositorios y la interacción con la base de datos.
- HotelReservation.Api: Define los endpoints para interactuar con la aplicación a través de HTTP.

## Tecnologías Utilizadas
- C# y .NET8: Lenguaje de programación y framework principal.
- Entity Framework Core: Para la interacción con la base de datos SQLServer.
- MediatR: Para la implementación de patrones CQRS.
 -Xunit: Para pruebas unitarias.

## Instalación y Configuración
- Clonar el Repositorio: git clone <URL del repositorio>
- Restaurar Paquetes: Ejecutar dotnet restore para restaurar los paquetes NuGet.
- Configurar la Base de Datos: Asegurarse de que la cadena de conexión en appsettings.json esté configurada correctamente.
- Configurar parametros SMTP: Asegurarse de plasmar los valores para la conexión SMTP para el envío de correos electrónicos
- Ejecutar Migraciones: dotnet ef database update para aplicar las migraciones de la base de datos.
- Ejecutar la Aplicación: dotnet run para iniciar la aplicación.

## Enums
ErrorType Enum: 
este enum representa los tipos de errores que pueden ocurrir en la aplicación.

- 0	Failure
- 1	Validation
- 2	Problem
- 3	NotFound
- 4	Conflict

Gender Enum: este enum representa los tipos de género.

- 0	Male
- 1	Female
- 2	Other

DocumentType Enum: este enum representa los tipos de documentos.

- 0	CC
- 1	TI
- 2	CE
- 3	PP

RoomType Enum: este enum representa los tipos de habitaciones.

- 0	Single
- 1	Double
- 2	Suite
- 3	Deluxe
- 4	Family

ReservationStatus Enum: este enum representa los estados de una reserva.

- 1	Active
- 2	CheckedIn
- 3	Cancelled
- 4	NoShow

## Uso
API Endpoints: La aplicación expone varios endpoints para interactuar con las funcionalidades de hoteles, habitaciones y reservas.
Pruebas: Ejecutar dotnet test para correr las pruebas unitarias.

Especialista en Ingeniría de Software Daniel Barros Agamez
