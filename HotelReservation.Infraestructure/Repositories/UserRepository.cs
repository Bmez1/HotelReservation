using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Infraestructure.DataBase;

using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infraestructure.Repositories;

public class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<User?> GetByUserNameWithRolesAndPermissionsAsync(string userName)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .SingleOrDefaultAsync(x => x.UserName == userName);

        return user;
    }
}
