using HotelReservation.Api.Extensions;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Passengers.GetPassengers;
using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Api.EndPoints.Passangers;

public static class MapPassengers
{
    public static RouteGroupBuilder MapPassengersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetPassengersQuery());
            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetPassengers);

        return (RouteGroupBuilder)endpoints;
    }
}
