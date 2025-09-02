using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixStaticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "PK_BookingId",
                keyValue: 1,
                column: "BookingDate",
                value: new DateOnly(2025, 9, 15));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "PK_BookingId",
                keyValue: 2,
                column: "BookingDate",
                value: new DateOnly(2025, 9, 16));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "PK_BookingId",
                keyValue: 1,
                column: "BookingDate",
                value: new DateOnly(2025, 9, 2));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "PK_BookingId",
                keyValue: 2,
                column: "BookingDate",
                value: new DateOnly(2025, 9, 3));
        }
    }
}
