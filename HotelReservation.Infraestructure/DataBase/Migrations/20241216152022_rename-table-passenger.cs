using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservation.Infraestructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class renametablepassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerReservation_Passenger_PassengersId",
                table: "PassengerReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger");

            migrationBuilder.RenameTable(
                name: "Passenger",
                newName: "Passengers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerReservation_Passengers_PassengersId",
                table: "PassengerReservation",
                column: "PassengersId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerReservation_Passengers_PassengersId",
                table: "PassengerReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers");

            migrationBuilder.RenameTable(
                name: "Passengers",
                newName: "Passenger");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerReservation_Passenger_PassengersId",
                table: "PassengerReservation",
                column: "PassengersId",
                principalTable: "Passenger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
