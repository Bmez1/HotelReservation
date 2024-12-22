using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infraestructure.Repositories;

public class RoleRepository(ApplicationDbContext dbContext) : IRoleRepository
{
    public async Task<Role?> GetByIdAsync(int id)
    {
        var role = await dbContext.Roles.SingleOrDefaultAsync(x => x.Id == id);
        return role;        
    }
}
