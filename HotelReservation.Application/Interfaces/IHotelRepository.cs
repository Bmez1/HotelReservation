using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IHotelRepository : IRepository<Hotel, Guid>;
