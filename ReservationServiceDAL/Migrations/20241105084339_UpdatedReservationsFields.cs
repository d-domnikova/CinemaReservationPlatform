using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationServiceDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservationsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 5, 8, 43, 36, 509, DateTimeKind.Utc).AddTicks(3659));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 5, 8, 43, 36, 509, DateTimeKind.Utc).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 5, 8, 43, 36, 509, DateTimeKind.Utc).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 5, 8, 43, 36, 509, DateTimeKind.Utc).AddTicks(5212));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(7406));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8757));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeOfReservation",
                value: new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8766));
        }
    }
}
