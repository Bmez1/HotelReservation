namespace HotelReservation.Api.EndPoints.Users.Request;

public sealed record LoginRequest(string UserName, string Password);
