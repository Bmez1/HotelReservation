# HotelReservation
El proyecto "HotelReservation" es una aplicaci�n dise�ada para gestionar reservas de hoteles. Incluye funcionalidades para manejar hoteles, habitaciones, reservas y pasajeros. La aplicaci�n est� estructurada en varias capas, 
incluyendo la capa de dominio, aplicaci�n e infraestructura.

El proyecto aplica Arquitectura limpia y sigue principios de Domain-Driven Design, con una clara separaci�n de las capas de dominio, aplicaci�n e infraestructura, y el uso de patrones comunes como entidades, repositorios y objetos de valor.

## Patrones de Dise�o
- Repositorio (Repository Pattern)
- Inyecci�n de Dependencias (Dependency Injection)
- CQRS (Command Query Responsibility Segregation)
- SOLID

## Estructura del Proyecto
- HotelReservation.Domain: Contiene las entidades principales como Hotel, Room, Reservation, y sus respectivos atributos y m�todos.
- HotelReservation.Application: Incluye los casos de uso y DTOs (Data Transfer Objects) para manejar la l�gica de negocio y la comunicaci�n entre las capas.
- HotelReservation.Infraestructure: Proporciona la implementaci�n de los repositorios y la interacci�n con la base de datos.
- HotelReservation.Api: Define los endpoints para interactuar con la aplicaci�n a trav�s de HTTP.

## Tecnolog�as Utilizadas
- C# y .NET8: Lenguaje de programaci�n y framework principal.
- Entity Framework Core: Para la interacci�n con la base de datos SQLServer.
- MediatR: Para la implementaci�n de patrones CQRS.
 -Xunit: Para pruebas unitarias.

## Instalaci�n y Configuraci�n
- Clonar el Repositorio: git clone <URL del repositorio>
- Restaurar Paquetes: Ejecutar dotnet restore para restaurar los paquetes NuGet.
- Configurar la Base de Datos: Asegurarse de que la cadena de conexi�n en appsettings.json est� configurada correctamente.
- Configurar parametros SMTP: Asegurarse de plasmar los valores para la conexi�n SMTP para el env�o de correos electr�nicos
- Ejecutar Migraciones: dotnet ef database update para aplicar las migraciones de la base de datos.
- Ejecutar la Aplicaci�n: dotnet run para iniciar la aplicaci�n.

## Enums
ErrorType Enum: 
este enum representa los tipos de errores que pueden ocurrir en la aplicaci�n.

- 0	Failure
- 1	Validation
- 2	Problem
- 3	NotFound
- 4	Conflict

Gender Enum: este enum representa los tipos de g�nero.

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
API Endpoints: La aplicaci�n expone varios endpoints para interactuar con las funcionalidades de hoteles, habitaciones y reservas.
Pruebas: Ejecutar dotnet test para correr las pruebas unitarias.

Especialista en Ingenir�a de Software Daniel Barros Agamez
