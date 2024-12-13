using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

namespace HotelReservation.Infraestructure.Repositories;
public class HotelRepository : Repository<Hotel, Guid>, IHotelRepository
{
    public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
