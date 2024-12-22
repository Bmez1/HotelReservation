﻿using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public record CreateReservationCommand(Guid HotelId,
        Guid RoomId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        int NumberOfGuests,
        string EmergencyContactFullName,
        string EmergencyContactPhoneNumber) : IRequest<Result<string>>;
}
