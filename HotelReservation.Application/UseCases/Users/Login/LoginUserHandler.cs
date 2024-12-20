using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.Login;

internal sealed class LoginUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(u => u.UserName == command.UserName);

        if (user is null)
        {
            return Result.Failure<string>(UserError.NotFoundByUserName);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserError.NotFoundByUserName);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
