namespace HotelReservation.Application.UseCases.Hotels.Dtos
{
    public class HotelResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public long Phone { get; init; }
        public string? Description { get; init; }
        public bool IsEnabled { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
