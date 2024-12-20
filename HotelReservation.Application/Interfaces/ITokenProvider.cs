using HotelReservation.Domain.Entities;

namespace HotelReservation.Application.Interfaces;

public interface ITokenProvider
{
    string Create(User user);
}