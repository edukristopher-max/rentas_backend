using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPredios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCatastral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaTerreno = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreaConstruida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsoPredio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorArancelario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predios_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predios_ContribuyenteId",
                table: "Predios",
                column: "ContribuyenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Predios");
        }
    }
}
