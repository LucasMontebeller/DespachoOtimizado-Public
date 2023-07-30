using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespachoOtimizado.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateEstrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstradaSubTipo",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstradaSubTipo_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstradaTipo",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstradaTipo_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VelocidadeMedia = table.Column<decimal>(type: "numeric(4,1)", nullable: false),
                    EstradaTipoId = table.Column<byte>(type: "tinyint", nullable: false),
                    EstradaSubTipoId = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estrada_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estrada_EstradaSubTipo",
                        column: x => x.EstradaSubTipoId,
                        principalTable: "EstradaSubTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estrada_EstradaTipo",
                        column: x => x.EstradaTipoId,
                        principalTable: "EstradaTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estrada_EstradaSubTipoId",
                table: "Estrada",
                column: "EstradaSubTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estrada_EstradaTipoId",
                table: "Estrada",
                column: "EstradaTipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estrada");

            migrationBuilder.DropTable(
                name: "EstradaSubTipo");

            migrationBuilder.DropTable(
                name: "EstradaTipo");
        }
    }
}
