using Common;

namespace HotelReservation.Domain.Entities;
public sealed class Hotel : EntityBase<Guid>
{
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public long Phone { get; private set; }
    public string? Description { get; private set; }
    public List<Room> Rooms { get; private set; } = [];
    public List<Reservation> Reservations { get; private set; } = [];
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

    public void Update(string name, string country, long phone, string city, string? description)
    {
        Name = name;
        Country = country;
        City = city;
        Description = description;
        Phone = phone;
        IsEnabled = true;
    }

    public void ToggleStatus(bool? state = null)
    {
        IsEnabled = state is null ? !IsEnabled : state.Value;

        if (!IsEnabled)
            Rooms.ForEach(x => x.Deactivate("Hotel is disabled"));
    }

    public IEnumerable<Room> GetRooms(bool all = false) => all ? Rooms : Rooms.Where(x => x.IsEnabled).ToList();

    public static Hotel Create(string name, string country, long phone, string city, string? description) =>
        new(Guid.NewGuid(), name.ToUpper(), country.ToUpper(), phone, city.ToUpper(), description, DateTime.UtcNow);

}
