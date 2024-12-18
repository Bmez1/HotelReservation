using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Reservations.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.GetReservations
{
    public class GetReservationsHandler(IReservationRepository reservationRepository) : IRequestHandler<GetReservationsQuery, Result<IEnumerable<ReservationResponseDto>>>
    {
        public async Task<Result<IEnumerable<ReservationResponseDto>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await reservationRepository.ListAsync();

            var reservationsDto = reservations
                .AsEnumerable()
                .Select(reservation => new ReservationResponseDto
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Id = reservation.Id,
                RoomId = reservation.RoomId,
                EmergencyContact = reservation.EmergencyContact,
                HotelId = reservation.HotelId,
                NumberOfGuests = reservation.NumberOfGuests,
                ReservationStatus = reservation.ReservationStatus.ToString(),
                PassengerCount = reservation.PassengerCount,
                TravelerId = reservation.TravelerId,
                CreatedAt = reservation.CreatedAt                
            });

            return Result.Success(reservationsDto, totalData: reservationsDto.Count());
        }
    }
}
