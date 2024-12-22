using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.Dtos;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infraestructure.Repositories;
public class HotelRepository : Repository<Hotel, Guid>, IHotelRepository
{
    public HotelRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<HotelsForReservationResponseDto>> GetHotelsForReservationAsync(string city, DateTime checkIn, DateTime checkOut, int NumberOfGuests)
    {
        var query = from hotel in _dbContext.Hotels.AsNoTracking()
                    join room in _dbContext.Rooms.AsNoTracking() on hotel.Id equals room.HotelId
                    join reservation in _dbContext.Reservations.AsNoTracking() 
                        on room.Id equals reservation.RoomId into reservationsGroup
                    from reservation in reservationsGroup.DefaultIfEmpty()
                    where room.IsEnabled && 
                    hotel.IsEnabled &&
                    (city == null || hotel.City == city) &&
                    room.Capacity >= NumberOfGuests &&
                    (reservation == null || 
                    (reservation.CheckOutDate <= checkIn || reservation.CheckInDate >= checkOut))
                    select new HotelsForReservationResponseDto()
                    {
                        HotelId = hotel.Id,
                        RoomId = room.Id,
                        HotelName = hotel.Name,
                        Location = room.Location,
                        City = hotel.City,
                        Phone = hotel.Phone,
                        Type = room.Type,
                        RoomNumber = room.Number,
                        BaseCost = room.BaseCost,
                        Taxes = room.Taxes,
                        Capacity = room.Capacity,
                        BedCount = room.BedCount
                    };

        return await query
            .OrderBy(x => x.HotelName)
            .ToListAsync();                    
    }
}
