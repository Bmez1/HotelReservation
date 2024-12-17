using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservation.Infraestructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddfieldpassagerCountreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerCount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerCount",
                table: "Reservations");
        }
    }
}
