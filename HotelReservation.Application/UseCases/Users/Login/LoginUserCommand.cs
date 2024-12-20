using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.Login;

public sealed record LoginUserCommand(string UserName, string Password) : IRequest<Result<string>>;
