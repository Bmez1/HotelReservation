using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.UseCases.Passengers.Dtos;

public record PassengerResponseDto(
    Guid Id,
    string FullName,
    DateTime DateOfBirth,
    Gender Gender,
    string DocumentType,
    string DocumentNumber,
    string Email,
    string PhoneNumber
);

