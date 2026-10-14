using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Fase4B1_CajaUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CajasAperturas_UsuarioId",
                table: "CajasAperturas");

            migrationBuilder.CreateIndex(
                name: "IX_CajasAperturas_UsuarioId",
                table: "CajasAperturas",
                column: "UsuarioId",
                unique: true,
                filter: "[Estado] = N'Abierta'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CajasAperturas_UsuarioId",
                table: "CajasAperturas");

            migrationBuilder.CreateIndex(
                name: "IX_CajasAperturas_UsuarioId",
                table: "CajasAperturas",
                column: "UsuarioId",
                unique: true,
                filter: "[Estado] = 'Abierta'");
        }
    }
}
