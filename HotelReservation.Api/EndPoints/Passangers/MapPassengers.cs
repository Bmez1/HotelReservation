using HotelReservation.Api.EndPoints.Passangers.Request;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Passengers.CreatePassenger;
using HotelReservation.Application.UseCases.Passengers.GetPassengers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Passangers;

public static class MapPassengers
{
    public static RouteGroupBuilder MapPassengersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async ([FromBody] CreatePassengerRequest request, IMediator mediator) =>
        {
            var createPassengerCommand = new CreatePassengerCommand(
                request.ReservationId,
                request.FullName,
                request.DateOfBirth,
                request.Gender,
                request.DocumentType,
                request.DocumentNumber,
                request.Email,
                request.PhoneNumber
                );
            var result = await mediator.Send(createPassengerCommand);
            return result.ToHttpResponse();
        });

        endpoints.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetPassengersQuery());
            return result.ToHttpResponse();
        });

        return (RouteGroupBuilder)endpoints;
    }
}
