using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespachoOtimizado.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationNewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Custo",
                table: "Rota",
                type: "numeric(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tempo",
                table: "Rota",
                type: "numeric(4,1)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Custo",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "Rota");
        }
    }
}
