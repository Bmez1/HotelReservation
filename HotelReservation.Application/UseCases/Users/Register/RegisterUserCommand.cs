using Common;

using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.Register;

public sealed record RegisterUserCommand(string UserName, string Email, string FirstName, string LastName, string Password, Roles RoleId)
    : IRequest<Result<Guid>>;
