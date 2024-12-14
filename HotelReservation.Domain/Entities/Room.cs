using Common;

using HotelReservation.Domain.Enums;

namespace HotelReservation.Domain.Entities;

public class Room : EntityBase<Guid>
{
    public Guid HotelId { get; private set; }
    public string Number { get; private set; }
    public decimal BaseCost { get; private set; }
    public decimal Taxes { get; private set; }
    public string Location { get; set; }
    public RoomType Type { get; private set; }
    public bool IsEnabled { get; private set; } = true;

    private Room(Guid id, string number, decimal baseCost, decimal taxes, string location, RoomType type, Guid hotelId, DateTime createdAt)
    {
        Id = id;
        Number = number;
        BaseCost = baseCost;
        Taxes = taxes;
        Location = location;
        Type = type;
        HotelId = hotelId;
        CreatedAt = createdAt;
    }

    public static Room Create(string number, decimal baseCost, decimal taxes, string location, RoomType type, Guid hotelId) =>
        new(Guid.NewGuid(), number, baseCost, taxes, location, type, hotelId, DateTime.UtcNow);

    public void Update(decimal newBaseCost, decimal newTaxes, RoomType newType, string newNumber, string newLocation)
    {
        BaseCost = newBaseCost;
        Taxes = newTaxes;
        Type = newType;
        Number = newNumber;
        Location = newLocation;
    }

    public void Activate() => IsEnabled = true;

    public void Deactivate() => IsEnabled = false;
}

