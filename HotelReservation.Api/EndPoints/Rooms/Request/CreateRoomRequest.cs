using HotelReservation.Domain.Enums;

namespace HotelReservation.Api.EndPoints.Rooms.Request
{
    public class CreateRoomRequest
    {
        public string RoomNumber { get; init; } = string.Empty;
        public RoomType RoomType { get; init; }
        public decimal BaseCost { get; init; }
        public decimal Taxes { get; init; }
        public string Location { get; init; } = string.Empty;
        public int BedCount { get; init; }
        public int Capacity { get; init; }
    }
}
