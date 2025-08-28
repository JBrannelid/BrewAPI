using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    PK_CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.PK_CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    PK_MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.PK_MenuItemId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    PK_TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.PK_TableId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    PK_BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_CustomerId = table.Column<int>(type: "int", nullable: false),
                    FK_TableId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BookingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    NumberGuests = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Confirmed"),
                    DurationTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.PK_BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_FK_CustomerId",
                        column: x => x.FK_CustomerId,
                        principalTable: "Customers",
                        principalColumn: "PK_CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_FK_TableId",
                        column: x => x.FK_TableId,
                        principalTable: "Tables",
                        principalColumn: "PK_TableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "PK_CustomerId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "test@exempel.com", "Test Testsson", "070-1234567" },
                    { 2, "tian@exempel.com", "Tian Tiansson", "076-1234567" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 1, "Salads", "Handskalade räkor, bacon, cocktailtomat, rostade kruttonger, picklad rödlök, grana padano", "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400", true, "Ceasarsallad Räkor", 145.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Salads", "Rostade valnötter, fikon, rödbetor, äpple och rädisor, cocktailtomater, honung, ärtskott.", "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=400", "Chevré Salad", 125.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "Desserts", "Fransk crêpe med pistagekräm, choklad och vaniljglass", "https://images.unsplash.com/photo-1506459225024-1428097a7e18?w=400", true, "Dubai chocolate crêpe", 85.00m },
                    { 4, "Beverages", "Krämig cappuccino med perfekt mjölkskum", "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=400", true, "Cappuccino", 45.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 5, "Beverages", "Grönt, svart eller rött té i olika smaker", "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=400", "Tea Selection", 40.00m });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "PK_TableId", "Capacity", "IsAvailable", "TableNumber" },
                values: new object[,]
                {
                    { 1, 2, true, 1 },
                    { 2, 4, true, 2 },
                    { 3, 6, true, 3 },
                    { 4, 8, true, 4 },
                    { 5, 2, true, 5 },
                    { 6, 4, true, 6 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "Role" },
                values: new object[] { 1, "test@exempel.com", "Johannes", "Brannelid", "$2a$11$8Xl3E5qDNqK0rV2QcMkV4eJ1Q8P7K9zF0nR6tY3sW2hA5cU1mB7dO", "Admin" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "PK_BookingId", "BookingDate", "BookingTime", "DurationTime", "FK_CustomerId", "FK_TableId", "NumberGuests", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 8, 29), new TimeOnly(18, 30, 0), new TimeSpan(0, 2, 0, 0, 0), 1, 1, 2, "Confirmed" },
                    { 2, new DateOnly(2025, 8, 30), new TimeOnly(19, 0, 0), new TimeSpan(0, 2, 0, 0, 0), 2, 3, 4, "Confirmed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_CustomerId",
                table: "Bookings",
                column: "FK_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_TableId",
                table: "Bookings",
                column: "FK_TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
