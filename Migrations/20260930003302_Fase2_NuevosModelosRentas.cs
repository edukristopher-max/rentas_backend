using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Fase2_NuevosModelosRentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsHistoricoMigrado",
                table: "Pagos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLectura",
                table: "Notificaciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Leida",
                table: "Notificaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LicenciaTasaId",
                table: "Notificaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObligacionId",
                table: "Notificaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PagoId",
                table: "Notificaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PredioId",
                table: "Notificaciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditoriaLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Entidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntidadId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValoresAnterioresJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValoresNuevosJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CajasAperturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoApertura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajasAperturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CajasAperturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obligaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: false),
                    PredioId = table.Column<int>(type: "int", nullable: true),
                    ImpuestoPredialId = table.Column<int>(type: "int", nullable: true),
                    ArbitrioMunicipalId = table.Column<int>(type: "int", nullable: true),
                    LicenciaTasaId = table.Column<int>(type: "int", nullable: true),
                    OrigenReferencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Periodo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EsHistoricoMigrado = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obligaciones", x => x.Id);
                    table.CheckConstraint("CK_Obligacion_OrigenUnico_O_Historico", "(EsHistoricoMigrado = 0 AND (\n                    (ImpuestoPredialId IS NOT NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NULL) OR\n                    (ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NOT NULL AND LicenciaTasaId IS NULL) OR\n                    (ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NOT NULL)\n                ))\n                OR\n                (EsHistoricoMigrado = 1 AND ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NULL)");
                    table.ForeignKey(
                        name: "FK_Obligaciones_ArbitriosMunicipales_ArbitrioMunicipalId",
                        column: x => x.ArbitrioMunicipalId,
                        principalTable: "ArbitriosMunicipales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Obligaciones_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obligaciones_ImpuestosPrediales_ImpuestoPredialId",
                        column: x => x.ImpuestoPredialId,
                        principalTable: "ImpuestosPrediales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Obligaciones_LicenciasTasas_LicenciaTasaId",
                        column: x => x.LicenciaTasaId,
                        principalTable: "LicenciasTasas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Obligaciones_Predios_PredioId",
                        column: x => x.PredioId,
                        principalTable: "Predios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrediosContribuyentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredioId = table.Column<int>(type: "int", nullable: false),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: false),
                    TipoRelacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PorcentajeParticipacion = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrediosContribuyentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrediosContribuyentes_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrediosContribuyentes_Predios_PredioId",
                        column: x => x.PredioId,
                        principalTable: "Predios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recibos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagoId = table.Column<int>(type: "int", nullable: false),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recibos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recibos_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CajasCierres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CajaAperturaId = table.Column<int>(type: "int", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoSistema = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoDeclarado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Diferencia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioResponsableId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajasCierres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CajasCierres_CajasAperturas_CajaAperturaId",
                        column: x => x.CajaAperturaId,
                        principalTable: "CajasAperturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CajasCierres_Usuarios_UsuarioResponsableId",
                        column: x => x.UsuarioResponsableId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CajasMovimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CajaAperturaId = table.Column<int>(type: "int", nullable: false),
                    PagoId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajasMovimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CajasMovimientos_CajasAperturas_CajaAperturaId",
                        column: x => x.CajaAperturaId,
                        principalTable: "CajasAperturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CajasMovimientos_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObligacionesDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObligacionId = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObligacionesDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObligacionesDetalles_Obligaciones_ObligacionId",
                        column: x => x.ObligacionId,
                        principalTable: "Obligaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagoId = table.Column<int>(type: "int", nullable: false),
                    ObligacionId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagoDetalles_Obligaciones_ObligacionId",
                        column: x => x.ObligacionId,
                        principalTable: "Obligaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagoDetalles_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_LicenciaTasaId",
                table: "Notificaciones",
                column: "LicenciaTasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ObligacionId",
                table: "Notificaciones",
                column: "ObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_PagoId",
                table: "Notificaciones",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_PredioId",
                table: "Notificaciones",
                column: "PredioId");

            migrationBuilder.CreateIndex(
                name: "IX_CajasAperturas_UsuarioId",
                table: "CajasAperturas",
                column: "UsuarioId",
                unique: true,
                filter: "[Estado] = 'Abierta'");

            migrationBuilder.CreateIndex(
                name: "IX_CajasCierres_CajaAperturaId",
                table: "CajasCierres",
                column: "CajaAperturaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CajasCierres_UsuarioResponsableId",
                table: "CajasCierres",
                column: "UsuarioResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_CajasMovimientos_CajaAperturaId",
                table: "CajasMovimientos",
                column: "CajaAperturaId");

            migrationBuilder.CreateIndex(
                name: "IX_CajasMovimientos_PagoId",
                table: "CajasMovimientos",
                column: "PagoId",
                unique: true,
                filter: "[PagoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_ArbitrioMunicipalId",
                table: "Obligaciones",
                column: "ArbitrioMunicipalId",
                unique: true,
                filter: "[ArbitrioMunicipalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_ContribuyenteId",
                table: "Obligaciones",
                column: "ContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_Estado",
                table: "Obligaciones",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_FechaVencimiento",
                table: "Obligaciones",
                column: "FechaVencimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_ImpuestoPredialId",
                table: "Obligaciones",
                column: "ImpuestoPredialId",
                unique: true,
                filter: "[ImpuestoPredialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_LicenciaTasaId",
                table: "Obligaciones",
                column: "LicenciaTasaId",
                unique: true,
                filter: "[LicenciaTasaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Obligaciones_PredioId",
                table: "Obligaciones",
                column: "PredioId");

            migrationBuilder.CreateIndex(
                name: "IX_ObligacionesDetalles_ObligacionId",
                table: "ObligacionesDetalles",
                column: "ObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoDetalles_ObligacionId",
                table: "PagoDetalles",
                column: "ObligacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoDetalles_PagoId",
                table: "PagoDetalles",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_PrediosContribuyentes_ContribuyenteId",
                table: "PrediosContribuyentes",
                column: "ContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PrediosContribuyentes_PredioId_ContribuyenteId",
                table: "PrediosContribuyentes",
                columns: new[] { "PredioId", "ContribuyenteId" });

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_PagoId",
                table: "Recibos",
                column: "PagoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_LicenciasTasas_LicenciaTasaId",
                table: "Notificaciones",
                column: "LicenciaTasaId",
                principalTable: "LicenciasTasas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Obligaciones_ObligacionId",
                table: "Notificaciones",
                column: "ObligacionId",
                principalTable: "Obligaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Pagos_PagoId",
                table: "Notificaciones",
                column: "PagoId",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Predios_PredioId",
                table: "Notificaciones",
                column: "PredioId",
                principalTable: "Predios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_LicenciasTasas_LicenciaTasaId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Obligaciones_ObligacionId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Pagos_PagoId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Predios_PredioId",
                table: "Notificaciones");

            migrationBuilder.DropTable(
                name: "AuditoriaLogs");

            migrationBuilder.DropTable(
                name: "CajasCierres");

            migrationBuilder.DropTable(
                name: "CajasMovimientos");

            migrationBuilder.DropTable(
                name: "ObligacionesDetalles");

            migrationBuilder.DropTable(
                name: "PagoDetalles");

            migrationBuilder.DropTable(
                name: "PrediosContribuyentes");

            migrationBuilder.DropTable(
                name: "Recibos");

            migrationBuilder.DropTable(
                name: "CajasAperturas");

            migrationBuilder.DropTable(
                name: "Obligaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_LicenciaTasaId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_ObligacionId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_PagoId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_PredioId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "EsHistoricoMigrado",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "FechaLectura",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "Leida",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "LicenciaTasaId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "ObligacionId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "PagoId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "PredioId",
                table: "Notificaciones");
        }
    }
}
