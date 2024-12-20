using Common;

using HotelReservation.Application.UseCases.Users.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserResponseDto>>;
