using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservationServiceDAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    isCancelable = table.Column<bool>(type: "bit", nullable: false),
                    TimeOfReservation = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_TicketType",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 320m, "For adults" },
                    { 2, 175m, "For children" },
                    { 3, 250m, "For students" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Amount", "Date", "Name", "ShowtimeId", "TicketTypeId", "TimeOfReservation", "TotalCost", "isCancelable" },
                values: new object[,]
                {
                    { 1, 3, new DateOnly(2024, 11, 7), "Jane Smith", 2, 1, new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(7406), 0m, true },
                    { 2, 1, new DateOnly(2024, 11, 7), "Jane Smith", 2, 2, new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8757), 0m, true },
                    { 3, 2, new DateOnly(2024, 11, 12), "Mei Chao", 4, 1, new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8764), 0m, true },
                    { 4, 1, new DateOnly(2024, 11, 5), "Richard Cameron", 2, 3, new DateTime(2024, 11, 4, 21, 54, 58, 287, DateTimeKind.Utc).AddTicks(8766), 0m, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TicketTypeId",
                table: "Reservations",
                column: "TicketTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "TicketTypes");
        }
    }
}
