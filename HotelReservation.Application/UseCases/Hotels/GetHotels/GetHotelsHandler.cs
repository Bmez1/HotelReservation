using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.Dtos;
using HotelReservation.Domain.Entities;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotels;

public class GetHotelsHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelsQuery, Result<IEnumerable<HotelResponseDto>>>
{
    public async Task<Result<IEnumerable<HotelResponseDto>>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Hotel> hotels;
        if (request.All)
        {
            hotels = await hotelRepository.ListAsync();
        }
        else
        {
            hotels = await hotelRepository.ListAsync(hotel => hotel.IsEnabled);
        }

        var list = hotels.Select(hotel => new HotelResponseDto
        {
            Id = hotel.Id,
            City = hotel.City,
            Name = hotel.Name,
            Country = hotel.Country,
            CreatedAt = hotel.CreatedAt,
            Description = hotel.Description,
            IsEnabled = hotel.IsEnabled,
            Phone = hotel.Phone
        });

        return Result.Success(list, hotels.Count());
    }
}
