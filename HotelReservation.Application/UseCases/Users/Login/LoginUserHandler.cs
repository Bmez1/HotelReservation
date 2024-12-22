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
        var user = await userRepository.GetByUserNameWithRolesAndPermissionsAsync(command.UserName);

        if (user is null)
        {
            return Result.Failure<string>(UserError.NotFoundByUserName);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserError.NotFoundByUserName);
        }

        var rolesSet = user.Roles
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();

        string token = tokenProvider.Create(user, rolesSet);

        return token;
    }
}
