using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Fase4C_EstructuraPagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Seq_NumeroRecibo",
                startValue: 10000L);

            migrationBuilder.AlterColumn<int>(
                name: "DeudaId",
                table: "Pagos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pagos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Vigente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pagos");

            migrationBuilder.DropSequence(
                name: "Seq_NumeroRecibo");

            migrationBuilder.AlterColumn<int>(
                name: "DeudaId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
