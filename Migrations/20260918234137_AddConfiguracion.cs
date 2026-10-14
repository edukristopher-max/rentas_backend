using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddConfiguracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracionSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMunicipalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TasaInteresMoratorio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UITActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionSistema", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ConfiguracionSistema",
                columns: new[] { "Id", "Direccion", "EmailContacto", "Moneda", "NombreMunicipalidad", "Ruc", "TasaInteresMoratorio", "Telefono", "UITActual" },
                values: new object[] { 1, "Av. Principal 123", "contacto@municipalidad.gob.pe", "PEN", "Municipalidad Distrital", "20123456789", 1.20m, "(01) 555-1234", 5150.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionSistema");
        }
    }
}
