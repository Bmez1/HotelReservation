using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.UpdateHotel;

public class UpdateHotelHandler(IHotelRepository hotelRepository) : IRequestHandler<UpdateHotelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        Hotel? hotel = await hotelRepository.GetByIdAsync(request.HotelId, true);

        if (hotel is null)
        {
            return Result.Failure<Guid>(HotelError.NotFoundById);
        }

        // If the property is true, disable the hotel
        if (request.Disable)
        {
            hotel.ToggleStatus();
            await hotelRepository.SaveChangesAsync();
            return Result.Success(hotel.Id);
        }

        hotel.Update(request.Name, request.Country, request.Phone, request.City, request.Description);

        var existsHotel = await hotelRepository.ExistsAsync(hotel => hotel.Name.Equals(request.Name) &&
            hotel.City.Equals(request.City) &&
            hotel.Country.Equals(request.Country) && 
            hotel.Id != request.HotelId);

        if (existsHotel)
        {
            return Result.Failure<Guid>(HotelError.AlreadyExists);
        }

        await hotelRepository.SaveChangesAsync();

        return Result.Success(hotel.Id);
    }
}


