using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                values: new object[,]
                {
                    { 1, "Salads", "Handskalade räkor, bacon, cocktailtomat, rostade kruttonger, picklad rödlök, grana padano", "https://images.unsplash.com/photo-1551248429-40975aa4de74?w=400", true, "Ceasarsallad Räkor", 145.00m },
                    { 2, "Salads", "Svensk kyckling, caesar dressing, rödlök, ugnsbakade cocktailtomater, parmesan, & egenrostade krutonger", "https://images.unsplash.com/photo-1546793665-c74683f339c1?fm=jpg&q=60&w=3000&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D?w=400", true, "Ceasarsallad", 145.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "Salads", "Halstrad tonfiskfilé med sallad citron & korianderkräm", "https://images.unsplash.com/photo-1604909052743-94e838986d24?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D?w=400", "Tonfisksallad", 145.00m },
                    { 4, "Salads", "Rostade valnötter, fikon, rödbetor, äpple och rädisor, cocktailtomater, honung, ärtskott.", "https://images.unsplash.com/photo-1540420773420-3366772f4999?w=400", "Chevré Salad", 125.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 5, "Bowls", "Bulgogimarinerat högrev, bakat ägg, kimchi, morötter, spenat, böngroddar, furikake, sesamfrön, gochujangsås, sojamayo", "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Din-Din Bap Bowl", 188.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 6, "Bowls", "Kycklinglårfilé, ris, mango, teriyaksås, sojamajo, salladslök, togarashi, chili, soja, avokado, sojabönor, kimchi, picklad rödkål, smashed spicy cucumber", "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Sneaky Samurai Bowl", 164.00m },
                    { 7, "Bowls", "Kycklinglårfilé, ris, mango, teriyaksås, sojamajo, salladslök, togarashi, chili, soja, avokado, sojabönor, kimchi, picklad rödkål, smashed spicy cucumber", "https://plus.unsplash.com/premium_photo-1705056547195-a68c45f2d77e?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Zm9vZCUyMGJvd2x8ZW58MHx8MHx8fDA%3D", "Karaage Bowl", 164.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 8, "Desserts", "Fransk crêpe med pistagekräm, choklad och vaniljglass", "https://images.unsplash.com/photo-1723691802547-b79f65c16b5f?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Dubai chocolate crêpe", 85.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 9, "Desserts", "Frasig croissant bakad med smör", "https://images.unsplash.com/photo-1723691802547-b79f65c16b5f?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Croissant", 45.00m },
                    { 10, "Desserts", "Vetebulle bakad med surdeg, fylld med pumpafyllning och toppad med majssmulor.", "https://images.unsplash.com/photo-1589783361701-d5161a106415?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3DY", "Pumpkin Bun", 45.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[,]
                {
                    { 11, "Desserts", "Kladdkaka som serveras med vispgrädde och vaniljglass", "https://images.unsplash.com/photo-1705472017435-7a820b01f36c?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Mudcake", 65.00m },
                    { 12, "Desserts", "En rund kak- och gräddmousse med chokladsmulor", "https://plus.unsplash.com/premium_photo-1695028377713-f5e5424b1e7e?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Moussekaka Cookies & Cream", 65.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 13, "Beverages", "Krämig cappuccino med perfekt mjölkskum", "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=400", "Cappuccino", 45.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 14, "Beverages", "Sötat grönt matchate, serverat med kall havredryck och is", "https://images.unsplash.com/photo-1717603545758-88cc454db69b?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Ismatcha", 45.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 15, "Beverages", "Espresso blandat med kall mjölk och is", "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Iced Latte", 45.00m },
                    { 16, "Beverages", "Espresso blandat med mjölk", "https://images.unsplash.com/photo-1574914629385-46448b767aec?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Latte", 45.00m },
                    { 17, "Beverages", "Blonde Roast - Veranda Blend", "https://images.unsplash.com/photo-1610632380989-680fe40816c6?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Hot Coffee", 45.00m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 18, "Beverages", "Vaniljshake med smak av choklad. Toppad med vispgrädde", "https://images.unsplash.com/photo-1572490122747-3968b75cc699?q=80&w=774&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Caramel Frappuccino", 45.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 19, "Beverages", "Kaffeshake med smak av kola. Toppad med vispgrädde", "https://images.unsplash.com/photo-1637178035222-a08f2d4dd1a3?q=80&w=756&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Caramel Frappuccino", 45.00m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "PK_MenuItemId", "Category", "Description", "ImageUrl", "IsPopular", "Name", "Price" },
                values: new object[] { 20, "Beverages", "Grönt, svart eller rött té i olika smaker", "https://plus.unsplash.com/premium_photo-1674406481284-43eba097a291?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Tea Selection", 40.00m });

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
                table: "Bookings",
                columns: new[] { "PK_BookingId", "BookingDate", "BookingTime", "DurationTime", "FK_CustomerId", "FK_TableId", "NumberGuests", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 9, 15), new TimeOnly(18, 30, 0), new TimeSpan(0, 2, 0, 0, 0), 1, 1, 2, "Confirmed" },
                    { 2, new DateOnly(2025, 9, 16), new TimeOnly(19, 0, 0), new TimeSpan(0, 2, 0, 0, 0), 2, 3, 4, "Confirmed" }
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
