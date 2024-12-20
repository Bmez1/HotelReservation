using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.Register;

public sealed record RegisterUserCommand(string UserName, string Email, string FirstName, string LastName, string Password)
    : IRequest<Result<Guid>>;
