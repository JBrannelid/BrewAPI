using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyEntityIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PK_TableId",
                table: "Tables",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PK_MenuItemId",
                table: "MenuItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PK_CustomerId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PK_BookingId",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tables",
                newName: "PK_TableId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MenuItems",
                newName: "PK_MenuItemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "PK_CustomerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "PK_BookingId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }
    }
}
