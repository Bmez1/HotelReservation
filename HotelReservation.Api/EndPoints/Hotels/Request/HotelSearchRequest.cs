namespace HotelReservation.Api.EndPoints.Hotels.Request;

public class HotelSearchRequest
{
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public int NumberOfGuests { get; init; }
    public string DestinationCity { get; init; } = string.Empty;
}