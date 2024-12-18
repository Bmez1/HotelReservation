using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Passengers.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Passengers.GetPassengers;

public class GetPassengersHandler(IPassengerRepository passengerRepository) : IRequestHandler<GetPassengersQuery, Result<IEnumerable<PassengerResponseDto>>>
{
    public async Task<Result<IEnumerable<PassengerResponseDto>>> Handle(GetPassengersQuery request, CancellationToken cancellationToken)
    {
        var passengers = await passengerRepository.ListAsync();
        var passengerDtos = passengers.Select(p => new PassengerResponseDto(
            p.Id,
            p.FullName,
            p.DateOfBirth,
            p.Gender.ToString(),
            p.DocumentType.ToString(),
            p.DocumentNumber,
            p.Email,
            p.PhoneNumber
            ));

        return Result.Success(passengerDtos, totalData: passengerDtos.Count());
    }
}