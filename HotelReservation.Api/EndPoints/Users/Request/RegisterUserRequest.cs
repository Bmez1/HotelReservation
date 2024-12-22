using HotelReservation.Domain.Enums;

namespace HotelReservation.Api.EndPoints.Users.Request;

public sealed record RegisterUserRequest(string Email, string UserName, string FirstName, string LastName, string Password, Roles RoleId);
