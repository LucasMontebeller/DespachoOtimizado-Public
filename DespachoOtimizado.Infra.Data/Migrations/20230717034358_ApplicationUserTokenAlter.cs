using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespachoOtimizado.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserTokenAlter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Expiration",
                table: "AspNetUsers",
                newName: "AccessTokenExpirationTime");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AccessTokenExpirationTime",
                table: "AspNetUsers",
                newName: "Expiration");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
