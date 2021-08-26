using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class demanda_prestadorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdPrestador",
                table: "Demanda",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrestadorId",
                table: "Demanda",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demanda_PrestadorId",
                table: "Demanda",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demanda_AspNetUsers_PrestadorId",
                table: "Demanda",
                column: "PrestadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demanda_AspNetUsers_PrestadorId",
                table: "Demanda");

            migrationBuilder.DropIndex(
                name: "IX_Demanda_PrestadorId",
                table: "Demanda");

            migrationBuilder.DropColumn(
                name: "IdPrestador",
                table: "Demanda");

            migrationBuilder.DropColumn(
                name: "PrestadorId",
                table: "Demanda");
        }
    }
}
