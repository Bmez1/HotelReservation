using Common;

namespace HotelReservation.Domain.Errors;

public static class UserError
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error Unauthorized => Error.Failure(
        "Users.Unauthorized",
        "You are only authorized to access your own information.");

    public static readonly Error NotFoundByUserName = Error.NotFound(
        "Users.NotFoundByUserName",
        "The user with the specified username was not found");

    public static readonly Error UserNameNotUnique = Error.Conflict(
        "Users.UserNameNotUnique",
        "The provided username is not unique");
}

