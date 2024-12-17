using HotelReservation.Api.EndPoints.Reservations.Request;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Reservations.CreateReservation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Reservations;

public static class MapReservation
{
    public static RouteGroupBuilder MapReservationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async () =>
        {
            await Task.Delay(1);
            return Results.Ok("Hotels");
        });

        endpoints.MapGet("/{id}", async (string id) =>
        {
            await Task.Delay(1);
            return Results.Ok($"Hotels {id}");
        });

        endpoints.MapPost("/", async ([FromBody] CreateReservationRequest request, IMediator mediator) =>
        {
            var command = new CreateReservationCommand(
                request.HotelId,
                request.RoomId,
                request.TravelerId,
                request.DestinationCity,
                request.CheckInDate,
                request.CheckOutDate,
                request.NumberOfGuests,
                request.EmergencyContactFullName,
                request.EmergencyContactPhoneNumber);

            var result = await mediator.Send(command);
            return result.ToHttpResponse();
        });

        return (RouteGroupBuilder)endpoints;
    }
}
