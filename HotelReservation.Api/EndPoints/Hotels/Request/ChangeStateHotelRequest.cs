namespace HotelReservation.Api.EndPoints.Hotels.Request;

public class ChangeStateHotelRequest
{
    public Guid HoltelId { get; init; }
    public bool Enable { get; init; }
}
