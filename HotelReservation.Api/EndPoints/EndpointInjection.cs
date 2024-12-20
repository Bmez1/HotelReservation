using HotelReservation.Api.EndPoints.Hotels;
using HotelReservation.Api.EndPoints.Passangers;
using HotelReservation.Api.EndPoints.Reservations;
using HotelReservation.Api.EndPoints.Rooms;
using HotelReservation.Api.EndPoints.Users;

namespace HotelReservation.Api.EndPoints;

public static class EndpointInjection
{
    public static void ConfigureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/Hotels").MapHotelsEndpoints().WithTags(Tags.Hotels).RequireAuthorization();
        app.MapGroup("/Users").MapUsersEndpoints().WithTags(Tags.Users);
        app.MapGroup("/Passengers").MapPassengersEndpoints().WithTags(Tags.Passengers).RequireAuthorization();
        app.MapGroup("/Reservations").MapReservationsEndpoints().WithTags(Tags.Reservations).RequireAuthorization();
        app.MapGroup("/Hotels/{hotelId:guid}/Rooms").MapRoomsEndpoints().WithTags(Tags.Rooms).RequireAuthorization();
    }
}
