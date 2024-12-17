using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotelsForReservation;

public class GetHotelsForReservationHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelsForReservationQuery, Result<IEnumerable<HotelsForReservationResponseDto>>>
{
    public async Task<Result<IEnumerable<HotelsForReservationResponseDto>>> Handle(GetHotelsForReservationQuery request, CancellationToken cancellationToken)
    {
        var hotels = await hotelRepository.GetHotelsForReservationAsync(request.DestinationCity, request.CheckInDate, request.CheckOutDate, request.NumberOfGuests);
        return Result.Success(hotels, totalData: hotels.Count());
    }
}


