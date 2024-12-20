using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Users.Dtos;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.GetById;

public sealed class GetUserByIdHandler(IUserRepository userRepository, IUserContext userContext)
    : IRequestHandler<GetUserByIdQuery, Result<UserResponseDto>>
{
    public async Task<Result<UserResponseDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<UserResponseDto>(UserError.Unauthorized);
        }

        var user = await userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Result.Failure<UserResponseDto>(UserError.NotFound(query.UserId));
        }

        var userDto = new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName
        };

        return userDto;
    }
}
