using HotelReservation.Api.EndPoints.Hotels;
using HotelReservation.Api.EndPoints.Passangers;
using HotelReservation.Api.EndPoints.Reservations;
using HotelReservation.Api.EndPoints.Rooms;

namespace HotelReservation.Api.EndPoints;

public static class EndpointInjection
{
    public static void ConfigureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/Hotels").MapHotelsEndpoints().WithTags(Tags.Hotels);
        app.MapGroup("/Passengers").MapPassengersEndpoints().WithTags(Tags.Passengers);
        app.MapGroup("/Reservations").MapReservationsEndpoints().WithTags(Tags.Reservations);
        app.MapGroup("/Hotels/{hotelId:guid}/Rooms").MapRoomsEndpoints().WithTags(Tags.Rooms);
    }
}
