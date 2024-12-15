namespace HotelReservation.Api.EndPoints.Hotels.Request;

public class ChangeStateRoomRequest
{
    public Guid RoomId { get; init; }
    public bool Enable { get; init; }
    public string? ReasonDisable { get; init; }
}