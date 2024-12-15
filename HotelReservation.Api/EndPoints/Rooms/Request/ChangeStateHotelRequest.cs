namespace HotelReservation.Api.EndPoints.Rooms.Request;

public class ChangeStateHotelRequest
{
    public Guid HoltelId { get; init; }
    public bool Enable { get; init; }
}
