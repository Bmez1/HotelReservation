using HotelReservation.Api.EndPoints.Hotels.Request;
using HotelReservation.Api.Extensions;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Hotels.ChangeStateHotel;
using HotelReservation.Application.UseCases.Hotels.CreateHotel;
using HotelReservation.Application.UseCases.Hotels.GetHotelById;
using HotelReservation.Application.UseCases.Hotels.GetHotels;
using HotelReservation.Application.UseCases.Hotels.GetHotelsForReservation;
using HotelReservation.Application.UseCases.Hotels.UpdateHotel;
using HotelReservation.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Hotels;

public static class MapHotel
{
    public static RouteGroupBuilder MapHotelsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (IMediator mediator, bool all = false) =>
        {
            var result = await mediator.Send(new GetHotelsQuery(all));
            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetHotels);

        endpoints.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetHotelByIdQuery(id));
            return result.ToHttpResponse();
        });

        endpoints.MapPost("/", async ([FromBody] CreateHotelRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new CreateHotelCommand(
                request.Name,
                request.Country,
                request.City,
                request.Phone,
                request.Description));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetHotels);

        endpoints.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateHotelRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new UpdateHotelCommand(
                id,
                request.Name,
                request.Country,
                request.City,
                request.Phone,
                request.Description,
                request.Disable));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.UpdateHotel);

        endpoints.MapPut("/State", async ([FromBody] ChangeStateHotelRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new ChangeStateHotelCommand(
                request.HoltelId,
                request.Enable));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.UpdateHotel);

        endpoints.MapPost("/Rooms", async ([FromBody] HotelSearchRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetHotelsForReservationQuery(
                request.CheckInDate,
                request.CheckOutDate,
                request.NumberOfGuests,
                request.DestinationCity
                ));

            return result.ToHttpResponse();
        })
        .WithDescription("Endpoint used to retrieve available hotels with their rooms.")
        .HasPermission(Permissions.GetHotels);

        return (RouteGroupBuilder)endpoints;
    }
}
