using Common;

using HotelReservation.Application.UseCases.Passengers.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Passengers.GetPassengers;

public record GetPassengersQuery : IRequest<Result<IEnumerable<PassengerResponseDto>>>;
