namespace HotelReservation.Api.EndPoints.Hotels.Request;

public class CreateHotelRequest
{
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public long Phone { get; init; }
    public string? Description { get; init; }
}
