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
    public int BedCount { get; private set; }
    public int Capacity { get; private set; }
    public bool IsEnabled { get; private set; } = true;
    public string? DisableReason { get; private set; }
    public List<Reservation> Reservations { get; private set; } = [];

    private Room(
        Guid id, 
        string number, 
        decimal baseCost,
        decimal taxes,
        string location,
        RoomType type,
        Guid hotelId,
        int bedCount,
        int capacity,
        DateTime createdAt,
        string? disableReason = null)
    {
        Id = id;
        Number = number;
        BaseCost = baseCost;
        Taxes = taxes;
        Location = location;
        Type = type;
        HotelId = hotelId;
        BedCount = bedCount;
        Capacity = capacity;
        CreatedAt = createdAt;
        DisableReason = disableReason;
    }

    public static Room Create(
        string number,
        decimal baseCost,
        decimal taxes,
        string location,
        RoomType type,
        Guid hotelId,
        int bedCount,
        int capacity) =>
        new(Guid.NewGuid(), number, baseCost, taxes, location, type, hotelId, bedCount, capacity, DateTime.UtcNow);

    public void Update(decimal newBaseCost, decimal newTaxes, RoomType newType, string newNumber, string newLocation, int newBedCount, int newCapacity)
    {
        BaseCost = newBaseCost;
        Taxes = newTaxes;
        Type = newType;
        Number = newNumber;
        Location = newLocation;
        BedCount = newBedCount;
        Capacity = newCapacity;
    }

    public void Activate()
    {
        IsEnabled = true;
        DisableReason = null;
    }

    public void Deactivate(string reason)
    {
        IsEnabled = false;
        DisableReason = reason;
    }
}

