using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IRoleRepository
{
    public Task<Role?> GetByIdAsync(int id);
}
