using HotelReservation.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infraestructure.DataBase.Configurations;

internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Number).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Location).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DisableReason).HasMaxLength(80);

        builder.HasIndex(x => new { x.HotelId, x.Number })
            .IsUnique();
    }
}
