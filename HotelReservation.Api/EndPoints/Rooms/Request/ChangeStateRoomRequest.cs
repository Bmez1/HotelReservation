namespace HotelReservation.Api.EndPoints.Rooms.Request;

public class ChangeStateRoomRequest
{
    public Guid RoomId { get; init; }
    public bool Enable { get; init; }
    public string? ReasonDisable { get; init; }
}