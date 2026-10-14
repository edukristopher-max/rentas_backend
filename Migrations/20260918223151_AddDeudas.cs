using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDeudas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deudas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: false),
                    PredioId = table.Column<int>(type: "int", nullable: true),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeuda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deudas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deudas_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deudas_Predios_PredioId",
                        column: x => x.PredioId,
                        principalTable: "Predios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deudas_ContribuyenteId",
                table: "Deudas",
                column: "ContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Deudas_PredioId",
                table: "Deudas",
                column: "PredioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deudas");
        }
    }
}
