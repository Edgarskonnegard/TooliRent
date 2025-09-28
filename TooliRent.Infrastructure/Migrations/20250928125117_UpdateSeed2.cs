using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooliRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalPrice",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 12, 51, 16, 854, DateTimeKind.Utc).AddTicks(20));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 12, 51, 16, 854, DateTimeKind.Utc).AddTicks(2180));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalPrice",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 12, 41, 51, 385, DateTimeKind.Utc).AddTicks(1370));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 12, 41, 51, 385, DateTimeKind.Utc).AddTicks(5410));
        }
    }
}
