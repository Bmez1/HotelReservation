using Common;

using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Domain.Entities
{
    public class Reservation : EntityBase<Guid>
    {
        public Guid HotelId { get; private set; }
        public Guid RoomId { get; private set; }
        public Guid TravelerId { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public ReservationStatus ReservationStatus { get; private set; }
        public int NumberOfGuests { get; private set; }
        public EmergencyContact EmergencyContact { get; private set; }
        public List<Passenger> Passengers { get; private set; } = [];

        private Reservation(
            Guid id,
            Guid hotelId,
            Guid travelerId,
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            ReservationStatus reservationStatus,
            int numberOfGuests,
            EmergencyContact emergencyContact,
            DateTime createdAt)
        {
            Id = id;
            HotelId = hotelId;
            TravelerId = travelerId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            ReservationStatus = reservationStatus;
            NumberOfGuests = numberOfGuests;
            EmergencyContact = emergencyContact;
            CreatedAt = createdAt;
        }

        public static Reservation Create(
            Guid hotelId,
            Guid travelerId,
            Guid roomId,
            DateTime checkInDate,
            DateTime checkOutDate,
            ReservationStatus reservationStatus,
            int numberOfGuests,
            EmergencyContact emergencyContact) =>
            new(Guid.NewGuid(), hotelId, travelerId, roomId, checkInDate, checkOutDate, reservationStatus, numberOfGuests, emergencyContact, DateTime.UtcNow);

    }
}
