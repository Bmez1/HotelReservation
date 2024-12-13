namespace HotelReservation.Api.EndPoints.Reservations;

public static class MapReservation
{
    public static RouteGroupBuilder MapReservationssEndpoints(this IEndpointRouteBuilder endpoints)
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

        endpoints.MapPost("/", async () =>
        {
            await Task.Delay(1);
            return Results.Ok("Hotels");
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
