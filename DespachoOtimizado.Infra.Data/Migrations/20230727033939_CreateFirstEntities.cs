using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespachoOtimizado.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateFirstEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeiculoTipo",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoTipo_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VeiculoTipoId = table.Column<byte>(type: "tinyint", nullable: false),
                    Quantidade = table.Column<byte>(type: "tinyint", nullable: false),
                    Capacidade = table.Column<decimal>(type: "numeric(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_VeiculoTipo",
                        column: x => x.VeiculoTipoId,
                        principalTable: "VeiculoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_VeiculoTipoId",
                table: "Veiculo",
                column: "VeiculoTipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "VeiculoTipo");
        }
    }
}
