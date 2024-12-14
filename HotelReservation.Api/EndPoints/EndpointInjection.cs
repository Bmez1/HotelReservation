using HotelReservation.Api.EndPoints.Hotels;
using HotelReservation.Api.EndPoints.Reservations;

namespace HotelReservation.Api.EndPoints;

public static class EndpointInjection
{
    public static void ConfigureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/Hotels").MapHotelsEndpoints().WithTags(Tags.Hotels);
        app.MapGroup("/Reservations").MapReservationssEndpoints().WithTags(Tags.Reservations);
        app.MapGroup("/Hotels/{hotelId:guid}/Rooms").MapReservationssEndpoints().WithTags(Tags.Rooms);
    }
}
