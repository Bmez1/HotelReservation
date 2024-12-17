using Common;

using HotelReservation.Application.UseCases.Hotels.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotelById
{
    public record GetHotelByIdQuery(Guid HotelId) : IRequest<Result<HotelResponseDto>>;
}
