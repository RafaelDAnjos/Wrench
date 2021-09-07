using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class valor_cobrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorCobrado",
                table: "RegistroServico",
                newName: "ValorCobradoPrestador");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorCobradoDemandante",
                table: "RegistroServico",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorCobradoDemandante",
                table: "RegistroServico");

            migrationBuilder.RenameColumn(
                name: "ValorCobradoPrestador",
                table: "RegistroServico",
                newName: "ValorCobrado");
        }
    }
}
