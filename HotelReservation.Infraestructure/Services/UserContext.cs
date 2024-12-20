using HotelReservation.Application.Interfaces;
using HotelReservation.Infraestructure.Extensions;

using Microsoft.AspNetCore.Http;

namespace HotelReservation.Infraestructure.Services;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
}
