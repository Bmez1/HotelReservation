using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Errors;
using HotelReservation.Domain.ValueObjects;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public class CreateReservationHandler(IHotelRepository hotelRepository,
        IPassengerRepository passengerRepository,
        IReservationRepository reservationRepository,
        IUserContext userContext,
        IEmailSender emailSender) : IRequestHandler<CreateReservationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.UserId;
            var travelerEmail = userContext.Email;

            var hotels = (await hotelRepository.GetHotelsForReservationAsync(
                null!,
                request.CheckInDate,
                request.CheckOutDate,
                request.NumberOfGuests))
                .ToList();

            if (hotels.Count == 0 || hotels.Any(h => h.RoomId == request.RoomId))
            {
                return Result.Failure<string>(HotelError.NotAvailableForReservation);
            }

            var emergencyContact = new EmergencyContact(request.EmergencyContactFullName, request.EmergencyContactPhoneNumber);

            var reservation = Reservation.Create(
                request.HotelId,
                userId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                ReservationStatus.Active,
                request.NumberOfGuests,
                emergencyContact);

            await reservationRepository.AddAsync(reservation);
            await reservationRepository.SaveChangesAsync(cancellationToken);

            var msjSuccess = $"Reservation created with code: {reservation.Id}";

            await emailSender.SendEmailAsync(
                travelerEmail,
                "Your reservation has been successfully created.",
                $"Reservation created with code: {reservation.Id}");

            return Result.Success<string>(msjSuccess);

        }
    }
}
