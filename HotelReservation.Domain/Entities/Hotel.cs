using Common;

namespace HotelReservation.Domain.Entities;
public sealed class Hotel : EntityBase<Guid>
{
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public long Phone { get; private set; }
    public string? Description { get; private set; }
    public bool IsEnabled { get; private set; } = true;

    private Hotel(Guid id, string name, string country, long phone, string city, string? description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Description = description;
        Phone = phone;
        CreatedAt = createdAt;
    }

    public void ToggleStatus()
    {
        IsEnabled = !IsEnabled;
    }

    public static Hotel Create(string name, string country, long phone, string city, string? description) =>
        new(Guid.NewGuid(), name, country, phone, city, description, DateTime.UtcNow);
}
