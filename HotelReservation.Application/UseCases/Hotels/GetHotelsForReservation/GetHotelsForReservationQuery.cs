using Common;

using HotelReservation.Application.UseCases.Hotels.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotelsForReservation;

public record GetHotelsForReservationQuery(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int NumberOfGuests,
    string DestinationCity
    ) : IRequest<Result<IEnumerable<HotelsForReservationResponseDto>>>;


