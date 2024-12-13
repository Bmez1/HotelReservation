namespace HotelReservation.Api.EndPoints.Hotels.Request;

public class CreateHotelRequest
{
    public string Name { get; init; }
    public string Country { get; init; }
    public string City { get; init; }
    public long Phone { get; init; }
    public string? Description { get; init; }
}
