using Common;

using HotelReservation.Application.UseCases.Reservations.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.GetReservationbyId
{
    public record GetReservationByIdQuery(Guid ReservationId) : IRequest<Result<ReservationWithDetailsResponseDto>>;

}
