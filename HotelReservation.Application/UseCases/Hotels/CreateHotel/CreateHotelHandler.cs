using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.CreateHotel;

public class CreateHotelHandler(IHotelRepository hotelRepository) : IRequestHandler<CreateHotelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var existsHotel = await hotelRepository.ExistsAsync(hotel => hotel.Name.Equals(request.Name) &&
            hotel.City.Equals(request.City) &&
            hotel.Country.Equals(request.Country));

        if (existsHotel)
        {
            return Result.Failure<Guid>(HotelError.AlreadyExists);
        }

        var hotel = Hotel.Create(
            request.Name,
            request.Country,
            request.Phone,
            request.City,
            request.Description
            );

        await hotelRepository.AddAsync(hotel);
        await hotelRepository.SaveChangesAsync();
        return hotel.Id;
    }
}
