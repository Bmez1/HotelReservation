using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Passengers.Dtos;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Passengers.CreatePassenger;

public class CreatePassengerHandler(IPassengerRepository passengerRepository,
    IReservationRepository reservationRepository) : IRequestHandler<CreatePassengerCommand, Result<PassengerResponseDto>>
{
    public async Task<Result<PassengerResponseDto>> Handle(CreatePassengerCommand request, CancellationToken cancellationToken)
    {
        Reservation? reservation = null;

        if (request.ReservationId is not null)
        {
            reservation = await reservationRepository
                .GetByIdAsync(request.ReservationId.Value, true);

            if (reservation is null)
            {
                return Result.Failure<PassengerResponseDto>(ReservationError.NotFoundById);
            }
        }

        if (!reservation?.CanAddPassenger() ?? true)
        {
            return Result.Failure<PassengerResponseDto>(ReservationError.CannotAddPassenger);
        }

        var passenger = (await passengerRepository
            .ListAsync(p => p.DocumentNumber == request.DocumentNumber && p.DocumentType == request.DocumentType, true))
            .FirstOrDefault();

        if (passenger is not null)
        {
            passenger.Update(request.FullName, request.DateOfBirth, request.Gender, request.Email, request.PhoneNumber);
        }
        else
        {
            passenger = Passenger.Create(
                request.FullName,
                request.DateOfBirth,
                request.Gender,
                request.DocumentType,
                request.DocumentNumber,
                request.Email,
                request.PhoneNumber);

            await passengerRepository.AddAsync(passenger);
        }

        reservation?.AddPassenger(passenger);
        await passengerRepository.SaveChangesAsync();

        return new PassengerResponseDto(
            passenger.Id, 
            passenger.FullName,
            passenger.DateOfBirth,
            passenger.Gender,
            nameof(passenger.DocumentType), 
            passenger.DocumentNumber,
            passenger.Email,
            passenger.PhoneNumber
        );
    }
}

