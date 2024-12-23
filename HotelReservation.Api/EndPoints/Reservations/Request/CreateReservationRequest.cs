using HotelReservation.Api.EndPoints.Passangers.Request;

namespace HotelReservation.Api.EndPoints.Reservations.Request
{
    public class CreateReservationRequest
    {
        public Guid HotelId { get; init; }
        public Guid RoomId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public int NumberOfGuests { get; init; }
        public string EmergencyContactFullName { get; init; } = string.Empty;
        public string EmergencyContactPhoneNumber { get; init; } = string.Empty;

        public List<CreatePassengerRequest> Passengers { get; init; } = [];
    }
}
