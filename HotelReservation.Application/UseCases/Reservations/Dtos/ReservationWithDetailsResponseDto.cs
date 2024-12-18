using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Application.UseCases.Reservations.Dtos;

public class ReservationWithDetailsResponseDto
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public Guid RoomId { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Guid TravelerId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string ReservationStatus { get; set; } = string.Empty;
    public int NumberOfGuests { get; set; }
    public EmergencyContact EmergencyContact { get; set; } = null!;
    public List<PassengerResponseDto> Passengers { get; set; } = [];
    public int PassengerCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PassengerResponseDto
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
