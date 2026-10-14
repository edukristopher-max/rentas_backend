using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddLicencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenciasTasas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: false),
                    PredioId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroExpediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenciasTasas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenciasTasas_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenciasTasas_Predios_PredioId",
                        column: x => x.PredioId,
                        principalTable: "Predios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenciasTasas_ContribuyenteId",
                table: "LicenciasTasas",
                column: "ContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenciasTasas_PredioId",
                table: "LicenciasTasas",
                column: "PredioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenciasTasas");
        }
    }
}
