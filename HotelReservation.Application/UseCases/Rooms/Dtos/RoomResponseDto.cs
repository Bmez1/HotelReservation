namespace HotelReservation.Application.UseCases.Rooms.Dtos
{
    public class RoomResponseDto
    {
        public required Guid HotelId { get; set; }
        public required Guid Id { get; set; }
        public required string RoomNumber { get; set; } = string.Empty;
        public required decimal BaseCost { get; set; }
        public required decimal Taxes { get; set; }
        public required string Type { get; set; } = string.Empty;
        public required string Location { get; set; }
    }
}
