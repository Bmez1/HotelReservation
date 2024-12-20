using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IUserRepository : IRepository<User, Guid> { }
