using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Reservations.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.GetReservationbyId
{
    public class GetReservationByIdHandler(IReservationRepository reservationRepository) : IRequestHandler<GetReservationByIdQuery, Result<ReservationWithDetailsResponseDto>>
    {
        public async Task<Result<ReservationWithDetailsResponseDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await reservationRepository.GetReservationWithDetailsAsync(request.ReservationId);
            return reservation;
        }
    }
}
