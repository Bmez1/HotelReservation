using HotelReservation.Domain.Enums;
namespace HotelReservation.Api.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, Permissions permission)
    {
        return app.RequireAuthorization(permission.ToString());
    }
}
