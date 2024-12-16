using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

namespace HotelReservation.Infraestructure.Repositories;

public class PassengerRepository : Repository<Passenger, Guid>, IPassengerRepository
{
    public PassengerRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
