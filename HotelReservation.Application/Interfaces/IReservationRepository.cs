using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface IReservationRepository : IRepository<Reservation, Guid>;
