using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HotelReservation.Infraestructure.DataBase.Configurations;

internal sealed class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Traveler, Permissions.CreateReservation),
            Create(Role.Traveler, Permissions.GetReservations),
            Create(Role.Traveler, Permissions.CreatePassenger),
            Create(Role.TravelAgent, Permissions.GetReservations),
            Create(Role.TravelAgent, Permissions.CreatePassenger),
            Create(Role.TravelAgent, Permissions.GetPassengers),
            Create(Role.TravelAgent, Permissions.GetUsers),
            Create(Role.TravelAgent, Permissions.CreateHotel),
            Create(Role.TravelAgent, Permissions.UpdateHotel),
            Create(Role.TravelAgent, Permissions.GetHotels),
            Create(Role.TravelAgent, Permissions.CreateRoom),
            Create(Role.TravelAgent, Permissions.CreateReservation),
            Create(Role.TravelAgent, Permissions.GetRooms),
            Create(Role.TravelAgent, Permissions.CreateUser),
            Create(Role.TravelAgent, Permissions.UpdateRoom));
    }

    private static RolePermission Create(
        Role role, Permissions permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
