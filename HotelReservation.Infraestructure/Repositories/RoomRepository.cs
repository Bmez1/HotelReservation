using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

namespace HotelReservation.Infraestructure.Repositories;

public class RoomRepository : Repository<Room, Guid>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}

