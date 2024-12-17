using Common;

using HotelReservation.Application.UseCases.Reservations.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.GetReservations
{
    public record GetReservationsQuery : IRequest<Result<IEnumerable<ReservationResponseDto>>>;
}
