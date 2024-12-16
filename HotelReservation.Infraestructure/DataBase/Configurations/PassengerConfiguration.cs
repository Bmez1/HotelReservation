using HotelReservation.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infraestructure.DataBase.Configurations;

internal sealed class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passengers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(120);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(30);
    }
}
