using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Errors;
using HotelReservation.Domain.ValueObjects;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public class CreateReservationHandler(
        IPassengerRepository passengerRepository,
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IUserContext userContext,
        IEmailSender emailSender) : IRequestHandler<CreateReservationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.UserId;
            var travelerEmail = userContext.Email;

            #region Validations
            var existsReservation = await reservationRepository.ExistsAsync(r =>
                r.RoomId == request.RoomId &&
                r.ReservationStatus == ReservationStatus.Active &&
                r.CheckInDate < request.CheckOutDate &&
                r.CheckOutDate > request.CheckInDate);

            if (existsReservation)
            {
                return Result.Failure<string>(HotelError.NotAvailableForReservation);
            }

            Room? room = await roomRepository.FindAsync(r => r.Id == request.RoomId && r.HotelId == request.HotelId);

            if (room is null)
            {
                return Result.Failure<string>(RoomError.NotFound);
            }

            if (room.Capacity < request.NumberOfGuests || request.NumberOfGuests < request.Passengers.Count)
            {
                return Result.Failure<string>(ReservationError.CannotAddPassenger);
            }

            #endregion

            var emergencyContact = new EmergencyContact(request.EmergencyContactFullName, request.EmergencyContactPhoneNumber);

            Reservation reservation = Reservation.Create(
                request.HotelId,
                userId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                ReservationStatus.Active,
                request.NumberOfGuests,
                emergencyContact);

            var documents = request.Passengers.Select(p => p.DocumentNumber);

            // Add Passengers in Reservation
            var passengers = await passengerRepository.ListAsync(p => documents.Contains(p.DocumentNumber));
            var passengersDictionary = passengers.ToDictionary(p => $"{p.DocumentType}:{p.DocumentNumber}", p => p);
            List<Passenger> passengersToUpdate = [];
            List<Passenger> passengersToCreate = [];

            if (passengersDictionary.Count == 0)
            {
                request.Passengers.ForEach(p =>
                {
                    var passenger = Passenger.Create(p.FullName, p.DateOfBirth, p.Gender, p.DocumentType, p.DocumentNumber, p.Email, p.PhoneNumber);
                    passengersToCreate.Add(passenger);
                    reservation.AddPassenger(passenger);
                });
            }
            else
            {
                request.Passengers.ForEach(p =>
                {
                    if (passengersDictionary.TryGetValue($"{p.DocumentType}:{p.DocumentNumber}", out var passenger))
                    {
                        passenger.Update(
                            p.FullName,
                            p.DateOfBirth,
                            p.Gender,
                            p.Email,
                            p.PhoneNumber);

                        passengersToUpdate.Add(passenger);
                    }
                    else
                    {
                        passenger = Passenger.Create(p.FullName, p.DateOfBirth, p.Gender, p.DocumentType, p.DocumentNumber, p.Email, p.PhoneNumber);
                        passengersToCreate.Add(passenger);
                    }
                    reservation.AddPassenger(passenger);
                });
            }

            if (passengersToUpdate.Count > 0)
            {
                passengerRepository.EditRangeAsync(passengersToUpdate);
            }

            if (passengersToCreate.Count > 0)
            {
                await passengerRepository.AddRangeAsync(passengersToCreate);
            }

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
