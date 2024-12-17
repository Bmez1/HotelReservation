using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Reservations.Dtos;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infraestructure.Repositories;

public class ReservationRepository : Repository<Reservation, Guid>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public Task<ReservationWithDetailsResponseDto?> GetReservationWithDetailsAsync(Guid id)
    {
        var query = from reservation in _dbContext.Reservations.AsNoTracking()
                    join room in _dbContext.Rooms.AsNoTracking() on reservation.RoomId equals room.Id
                    join hotel in _dbContext.Hotels.AsNoTracking() on room.HotelId equals hotel.Id
                    where reservation.Id == id
                    select new ReservationWithDetailsResponseDto()
                    {
                        Id = reservation.Id,
                        CheckInDate = reservation.CheckInDate,
                        CheckOutDate = reservation.CheckOutDate,
                        RoomId = room.Id,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        Location = room.Location,
                        TravelerId = reservation.TravelerId,
                        RoomType = room.Type,
                        RoomNumber = room.Number,
                        EmergencyContact = reservation.EmergencyContact,
                        CreatedAt = reservation.CreatedAt,
                        NumberOfGuests = reservation.NumberOfGuests,
                        PassengerCount = reservation.Passengers.Count,
                        ReservationStatus = reservation.ReservationStatus,
                        Passengers = reservation.Passengers.Select(x => new PassengerResponseDto()
                        {
                            Id = x.Id,
                            DocumentType = x.DocumentType,
                            DateOfBirth = x.DateOfBirth,
                            DocumentNumber = x.DocumentNumber,
                            FullName = x.FullName,
                            Email = x.Email
                        }).ToList()
                    };

        return query.SingleOrDefaultAsync();
    }
}
