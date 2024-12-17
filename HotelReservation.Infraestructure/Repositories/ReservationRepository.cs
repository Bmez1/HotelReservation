using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

namespace HotelReservation.Infraestructure.Repositories;

public class ReservationRepository : Repository<Reservation, Guid>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
