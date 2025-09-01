using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "PK_MenuItemId",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?q=80&w=1160&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "PK_MenuItemId",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://unsplash.com/photos/vegetable-and-meat-on-bowl-kcA-c3f_3FE");
        }
    }
}
