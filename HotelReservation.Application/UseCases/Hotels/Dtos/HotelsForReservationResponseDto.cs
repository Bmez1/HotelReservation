using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.UseCases.Hotels.Dtos
{
    public class HotelsForReservationResponseDto
    {
        public required Guid HotelId { get; set; }
        public required Guid RoomId { get; set; }
        public required long Phone { get; set; }
        public required int BedCount { get; set; }
        public required int Capacity { get; set; }
        public required string HotelName { get; set; }
        public required string RoomNumber { get; set; } = string.Empty;
        public required decimal BaseCost { get; set; }
        public required decimal Taxes { get; set; }
        public required RoomType Type { get; set; }
        public required string Location { get; set; }
        public required string City { get; set; }
    }
}
