using HotelReservation.Api.EndPoints.Hotels.Request;
using HotelReservation.Application.UseCases.Hotels.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Hotels;

public static class MapHotel
{
    public static RouteGroupBuilder MapHotelsEndpoints(this IEndpointRouteBuilder endpoints)
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

        endpoints.MapPost("/", async ([FromBody] CreateHotelRequest request, IMediator mediator) =>
        {
            Guid result = await mediator.Send(new CreateCommand(
                request.Name,
                request.Country,
                request.City,
                request.Phone,
                request.Description));

            return Results.Ok(result);
        });

        endpoints.MapPut("/{id}", async () =>
        {
            await Task.Delay(1);
            return Results.Ok("Hotels");
        });

        endpoints.MapDelete("/{id}", async () =>
        {
            await Task.Delay(1);
            return Results.Ok("Hotels");
        });

        return (RouteGroupBuilder)endpoints;
    }
}
