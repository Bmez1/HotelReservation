using HotelReservation.Application.UseCases.Reservations.Dtos;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IReservationRepository : IRepository<Reservation, Guid>
{
    Task<ReservationWithDetailsResponseDto?> GetReservationWithDetailsAsync(Guid id);
}
