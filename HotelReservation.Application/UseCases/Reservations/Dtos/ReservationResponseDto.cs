using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.UseCases.Reservations.Dtos;

public class ReservationResponseDto
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomId { get; set; }
    public Guid TravelerId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string ReservationStatus { get; set; } = string.Empty;
    public int NumberOfGuests { get; set; }
    public EmergencyContact EmergencyContact { get; set; } = null!;
    public int PassengerCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
