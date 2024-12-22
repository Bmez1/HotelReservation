using Common;

namespace HotelReservation.Domain.Errors;

public static class RoleError
{
    public static Error NotFound(int roleId) => Error.NotFound(
        "Users.NotFound",
        $"The role with the Id = '{roleId}' was not found");
}

