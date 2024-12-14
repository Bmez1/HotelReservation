using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.Dtos;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotelById
{
    public class GetHotelByIdHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelByIdQuery, Result<HotelResponseDto>>
    {
        public async Task<Result<HotelResponseDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await hotelRepository.GetByIdAsync(request.HotelId);

            if (hotel is null)
            {
                return Result.Failure<HotelResponseDto>(HotelError.NotFoundById);
            }

            var hotelDto = new HotelResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Country = hotel.Country,
                CreatedAt = hotel.CreatedAt,
                Description = hotel.Description,
                IsEnabled = hotel.IsEnabled,
                Phone = hotel.Phone
            };

            return Result.Success(hotelDto);
        }
    }
}
