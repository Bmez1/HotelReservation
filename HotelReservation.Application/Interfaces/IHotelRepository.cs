using HotelReservation.Application.UseCases.Hotels.Dtos;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IHotelRepository : IRepository<Hotel, Guid>
{
    Task<IEnumerable<HotelsForReservationResponseDto>> GetHotelsForReservationAsync(string city, DateTime checkIn, DateTime checkOut, int NumberOfGuests);
}