using HotelReservation.Domain.Enums;

namespace HotelReservation.Api.EndPoints.Passangers.Request
{
    public class CreatePassengerRequest
    {
        public Guid? ReservationId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public DateTime DateOfBirth { get; init; }
        public Gender Gender { get; init; }
        public DocumentType DocumentType { get; init; }
        public string DocumentNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
}
