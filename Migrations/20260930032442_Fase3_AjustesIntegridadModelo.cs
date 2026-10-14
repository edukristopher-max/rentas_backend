using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Fase3_AjustesIntegridadModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitriosMunicipales_Predios_PredioId",
                table: "ArbitriosMunicipales");

            migrationBuilder.DropForeignKey(
                name: "FK_CajasMovimientos_CajasAperturas_CajaAperturaId",
                table: "CajasMovimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Deudas_Contribuyentes_ContribuyenteId",
                table: "Deudas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deudas_Predios_PredioId",
                table: "Deudas");

            migrationBuilder.DropForeignKey(
                name: "FK_ImpuestosPrediales_Predios_PredioId",
                table: "ImpuestosPrediales");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenciasTasas_Contribuyentes_ContribuyenteId",
                table: "LicenciasTasas");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenciasTasas_Predios_PredioId",
                table: "LicenciasTasas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Contribuyentes_ContribuyenteId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_ArbitriosMunicipales_ArbitrioMunicipalId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_Contribuyentes_ContribuyenteId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_ImpuestosPrediales_ImpuestoPredialId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_LicenciasTasas_LicenciaTasaId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_Predios_PredioId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ObligacionesDetalles_Obligaciones_ObligacionId",
                table: "ObligacionesDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_PagoDetalles_Pagos_PagoId",
                table: "PagoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Deudas_DeudaId",
                table: "Pagos");

            migrationBuilder.AlterColumn<decimal>(
                name: "AreaTerreno",
                table: "Predios",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AreaConstruida",
                table: "Predios",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TasaInteresMoratorio",
                table: "ConfiguracionSistema",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_NumeroComprobante",
                table: "Recibos",
                column: "NumeroComprobante",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitriosMunicipales_Predios_PredioId",
                table: "ArbitriosMunicipales",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CajasMovimientos_CajasAperturas_CajaAperturaId",
                table: "CajasMovimientos",
                column: "CajaAperturaId",
                principalTable: "CajasAperturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deudas_Contribuyentes_ContribuyenteId",
                table: "Deudas",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deudas_Predios_PredioId",
                table: "Deudas",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImpuestosPrediales_Predios_PredioId",
                table: "ImpuestosPrediales",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenciasTasas_Contribuyentes_ContribuyenteId",
                table: "LicenciasTasas",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenciasTasas_Predios_PredioId",
                table: "LicenciasTasas",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Contribuyentes_ContribuyenteId",
                table: "Notificaciones",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_ArbitriosMunicipales_ArbitrioMunicipalId",
                table: "Obligaciones",
                column: "ArbitrioMunicipalId",
                principalTable: "ArbitriosMunicipales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_Contribuyentes_ContribuyenteId",
                table: "Obligaciones",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_ImpuestosPrediales_ImpuestoPredialId",
                table: "Obligaciones",
                column: "ImpuestoPredialId",
                principalTable: "ImpuestosPrediales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_LicenciasTasas_LicenciaTasaId",
                table: "Obligaciones",
                column: "LicenciaTasaId",
                principalTable: "LicenciasTasas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_Predios_PredioId",
                table: "Obligaciones",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObligacionesDetalles_Obligaciones_ObligacionId",
                table: "ObligacionesDetalles",
                column: "ObligacionId",
                principalTable: "Obligaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PagoDetalles_Pagos_PagoId",
                table: "PagoDetalles",
                column: "PagoId",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Deudas_DeudaId",
                table: "Pagos",
                column: "DeudaId",
                principalTable: "Deudas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitriosMunicipales_Predios_PredioId",
                table: "ArbitriosMunicipales");

            migrationBuilder.DropForeignKey(
                name: "FK_CajasMovimientos_CajasAperturas_CajaAperturaId",
                table: "CajasMovimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Deudas_Contribuyentes_ContribuyenteId",
                table: "Deudas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deudas_Predios_PredioId",
                table: "Deudas");

            migrationBuilder.DropForeignKey(
                name: "FK_ImpuestosPrediales_Predios_PredioId",
                table: "ImpuestosPrediales");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenciasTasas_Contribuyentes_ContribuyenteId",
                table: "LicenciasTasas");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenciasTasas_Predios_PredioId",
                table: "LicenciasTasas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Contribuyentes_ContribuyenteId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_ArbitriosMunicipales_ArbitrioMunicipalId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_Contribuyentes_ContribuyenteId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_ImpuestosPrediales_ImpuestoPredialId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_LicenciasTasas_LicenciaTasaId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligaciones_Predios_PredioId",
                table: "Obligaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ObligacionesDetalles_Obligaciones_ObligacionId",
                table: "ObligacionesDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_PagoDetalles_Pagos_PagoId",
                table: "PagoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Deudas_DeudaId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Recibos_NumeroComprobante",
                table: "Recibos");

            migrationBuilder.AlterColumn<decimal>(
                name: "AreaTerreno",
                table: "Predios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AreaConstruida",
                table: "Predios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TasaInteresMoratorio",
                table: "ConfiguracionSistema",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitriosMunicipales_Predios_PredioId",
                table: "ArbitriosMunicipales",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CajasMovimientos_CajasAperturas_CajaAperturaId",
                table: "CajasMovimientos",
                column: "CajaAperturaId",
                principalTable: "CajasAperturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deudas_Contribuyentes_ContribuyenteId",
                table: "Deudas",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deudas_Predios_PredioId",
                table: "Deudas",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImpuestosPrediales_Predios_PredioId",
                table: "ImpuestosPrediales",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenciasTasas_Contribuyentes_ContribuyenteId",
                table: "LicenciasTasas",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenciasTasas_Predios_PredioId",
                table: "LicenciasTasas",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Contribuyentes_ContribuyenteId",
                table: "Notificaciones",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_ArbitriosMunicipales_ArbitrioMunicipalId",
                table: "Obligaciones",
                column: "ArbitrioMunicipalId",
                principalTable: "ArbitriosMunicipales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_Contribuyentes_ContribuyenteId",
                table: "Obligaciones",
                column: "ContribuyenteId",
                principalTable: "Contribuyentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_ImpuestosPrediales_ImpuestoPredialId",
                table: "Obligaciones",
                column: "ImpuestoPredialId",
                principalTable: "ImpuestosPrediales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_LicenciasTasas_LicenciaTasaId",
                table: "Obligaciones",
                column: "LicenciaTasaId",
                principalTable: "LicenciasTasas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligaciones_Predios_PredioId",
                table: "Obligaciones",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObligacionesDetalles_Obligaciones_ObligacionId",
                table: "ObligacionesDetalles",
                column: "ObligacionId",
                principalTable: "Obligaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PagoDetalles_Pagos_PagoId",
                table: "PagoDetalles",
                column: "PagoId",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Deudas_DeudaId",
                table: "Pagos",
                column: "DeudaId",
                principalTable: "Deudas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
