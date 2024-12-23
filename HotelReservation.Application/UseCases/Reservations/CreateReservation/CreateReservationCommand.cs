using Common;

using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation;

public record CreateReservationCommand(Guid HotelId,
    Guid RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int NumberOfGuests,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    List<CreatePassengerCommand> Passengers) : IRequest<Result<string>>;

public record CreatePassengerCommand(
    string FullName,
    DateTime DateOfBirth,
    Gender Gender,
    DocumentType DocumentType,
    string DocumentNumber,
    string Email,
    string PhoneNumber
    );
