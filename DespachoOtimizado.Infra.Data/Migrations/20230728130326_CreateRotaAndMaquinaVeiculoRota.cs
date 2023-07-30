using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespachoOtimizado.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRotaAndMaquinaVeiculoRota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrigemId = table.Column<int>(type: "int", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    RotaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rota_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rota_Destino_Localizacao",
                        column: x => x.DestinoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rota_Origem_Localizacao",
                        column: x => x.OrigemId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rota_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Rota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaquinaVeiculoRota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaquinaVeiculoId = table.Column<int>(type: "int", nullable: false),
                    RotaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaVeiculoRota_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaquinaVeiculoRota_MaquinaVeiculo",
                        column: x => x.MaquinaVeiculoId,
                        principalTable: "MaquinaVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaquinaVeiculoRota_Rota",
                        column: x => x.RotaId,
                        principalTable: "Rota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaVeiculoRota_MaquinaVeiculoId",
                table: "MaquinaVeiculoRota",
                column: "MaquinaVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaVeiculoRota_RotaId",
                table: "MaquinaVeiculoRota",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_DestinoId",
                table: "Rota",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_OrigemId",
                table: "Rota",
                column: "OrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_RotaId",
                table: "Rota",
                column: "RotaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaquinaVeiculoRota");

            migrationBuilder.DropTable(
                name: "Rota");
        }
    }
}
