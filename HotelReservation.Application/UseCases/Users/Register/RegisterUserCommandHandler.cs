using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.Register;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsAsync(u => u.UserName == command.UserName))
        {
            return Result.Failure<Guid>(UserError.UserNameNotUnique);
        }

        var user = User.Create(
            command.UserName,
            command.Email,
            command.FirstName,
            command.LastName,
            passwordHasher.Hash(command.Password)
            );

        await userRepository.AddAsync(user);

        await userRepository.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
