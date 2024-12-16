using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IPassengerRepository : IRepository<Passenger, Guid>;
