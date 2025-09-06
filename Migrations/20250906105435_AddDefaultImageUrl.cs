using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "MenuItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "https://plus.unsplash.com/premium_photo-1661349883108-3aea72f4a83f?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZW1wdHklMjBwbGF0ZXxlbnwwfHwwfHx8MA%3D%3D",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "MenuItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "https://plus.unsplash.com/premium_photo-1661349883108-3aea72f4a83f?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZW1wdHklMjBwbGF0ZXxlbnwwfHwwfHx8MA%3D%3D");
        }
    }
}
