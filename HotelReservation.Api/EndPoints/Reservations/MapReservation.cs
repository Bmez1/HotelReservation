using HotelReservation.Api.EndPoints.Reservations.Request;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Reservations.CreateReservation;
using HotelReservation.Application.UseCases.Reservations.GetReservationbyId;
using HotelReservation.Application.UseCases.Reservations.GetReservations;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Reservations;

public static class MapReservation
{
    public static RouteGroupBuilder MapReservationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetReservationsQuery());
            return result.ToHttpResponse();
        });

        endpoints.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetReservationByIdQuery(id));
            return result.ToHttpResponse();
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
        }).WithDescription("TravelerId is the ID of a passenger registered in the system.");

        return (RouteGroupBuilder)endpoints;
    }
}
