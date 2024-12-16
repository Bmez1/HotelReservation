using Common;

using HotelReservation.Application.UseCases.Passengers.Dtos;
using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Application.UseCases.Passengers.CreatePassenger;

public record CreatePassengerCommand(
    string FullName,
    DateTime DateOfBirth,
    Gender Gender,
    DocumentType DocumentType,
    string DocumentNumber,
    string Email,
    string PhoneNumber
    ) : IRequest<Result<PassengerResponseDto>>;

