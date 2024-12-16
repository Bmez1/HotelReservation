using HotelReservation.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infraestructure.DataBase.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CheckInDate).IsRequired();
        builder.Property(x => x.CheckOutDate).IsRequired();

        builder.OwnsOne(x => x.EmergencyContact, emergencyContact =>
        {
            emergencyContact.Property(x => x.FullName)
            .HasColumnName("EmergencyContactFullName")
            .IsRequired()
            .HasMaxLength(120);
            emergencyContact.Property(x => x.PhoneNumber)
            .HasColumnName("EmergencyContactPhoneNumber")
            .IsRequired()
            .HasMaxLength(30);
        });

        builder.HasOne<Room>()
        .WithMany(r => r.Reservations)
        .HasForeignKey(r => r.RoomId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Hotel>()
        .WithMany(h => h.Reservations)
        .HasForeignKey(r => r.HotelId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
        .HasMany(e => e.Passengers)
        .WithMany();
    }
}
