using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IRoomRepository : IRepository<Room, Guid>;
