using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.ChangeStateHotel
{
    public class ChangeStateHotelHandler(IHotelRepository hotelRepository) : IRequestHandler<ChangeStateHotelCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(ChangeStateHotelCommand request, CancellationToken cancellationToken)
        {
            Hotel? hotel = await hotelRepository.GetByIdAsync(request.HotelId, true);

            if (hotel is null)
            {
                return Result.Failure<Guid>(HotelError.NotFoundById);
            }

            if (request.Enable)
            {
                hotel.ToggleStatus(true);
                await hotelRepository.SaveChangesAsync();
                return Result.Success(hotel.Id);
            }

            hotel.ToggleStatus(false);
            await hotelRepository.SaveChangesAsync();
            return Result.Success(hotel.Id);
        }
    }
}
