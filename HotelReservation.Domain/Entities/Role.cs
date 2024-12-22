namespace HotelReservation.Domain.Entities;

public sealed class Role
{
    public readonly static Role TravelAgent = new(1, "TravelAgent");
    public readonly static Role Traveler = new(2, "Traveler");

    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<Permission> Permissions { get; private set; } = [];
    public List<User> Users { get; private set; } = [];

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
