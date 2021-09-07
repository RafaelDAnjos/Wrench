using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class escolher_demanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demanda_AspNetUsers_IdDemandante",
                table: "Demanda");

            migrationBuilder.DropForeignKey(
                name: "FK_Demanda_AspNetUsers_PrestadorId",
                table: "Demanda");

            migrationBuilder.DropIndex(
                name: "IX_RegistroServico_IdDemanda",
                table: "RegistroServico");

            migrationBuilder.DropIndex(
                name: "IX_Demanda_IdDemandante",
                table: "Demanda");

            migrationBuilder.DropIndex(
                name: "IX_Demanda_PrestadorId",
                table: "Demanda");

            migrationBuilder.DropColumn(
                name: "IdDemandante",
                table: "Demanda");

            migrationBuilder.DropColumn(
                name: "PrestadorId",
                table: "Demanda");

            migrationBuilder.RenameColumn(
                name: "IdPrestador",
                table: "Demanda",
                newName: "IdElaborador");

            migrationBuilder.AddColumn<Guid>(
                name: "IdDemandante",
                table: "RegistroServico",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPrestador",
                table: "RegistroServico",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Prazo",
                table: "RegistroServico",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RegistroServico_IdDemanda",
                table: "RegistroServico",
                column: "IdDemanda");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroServico_IdDemandante",
                table: "RegistroServico",
                column: "IdDemandante");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroServico_IdPrestador",
                table: "RegistroServico",
                column: "IdPrestador");

            migrationBuilder.CreateIndex(
                name: "IX_Demanda_IdElaborador",
                table: "Demanda",
                column: "IdElaborador");

            migrationBuilder.AddForeignKey(
                name: "FK_Demanda_AspNetUsers_IdElaborador",
                table: "Demanda",
                column: "IdElaborador",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroServico_AspNetUsers_IdDemandante",
                table: "RegistroServico",
                column: "IdDemandante",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroServico_AspNetUsers_IdPrestador",
                table: "RegistroServico",
                column: "IdPrestador",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demanda_AspNetUsers_IdElaborador",
                table: "Demanda");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroServico_AspNetUsers_IdDemandante",
                table: "RegistroServico");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroServico_AspNetUsers_IdPrestador",
                table: "RegistroServico");

            migrationBuilder.DropIndex(
                name: "IX_RegistroServico_IdDemanda",
                table: "RegistroServico");

            migrationBuilder.DropIndex(
                name: "IX_RegistroServico_IdDemandante",
                table: "RegistroServico");

            migrationBuilder.DropIndex(
                name: "IX_RegistroServico_IdPrestador",
                table: "RegistroServico");

            migrationBuilder.DropIndex(
                name: "IX_Demanda_IdElaborador",
                table: "Demanda");

            migrationBuilder.DropColumn(
                name: "IdDemandante",
                table: "RegistroServico");

            migrationBuilder.DropColumn(
                name: "IdPrestador",
                table: "RegistroServico");

            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "RegistroServico");

            migrationBuilder.RenameColumn(
                name: "IdElaborador",
                table: "Demanda",
                newName: "IdPrestador");

            migrationBuilder.AddColumn<Guid>(
                name: "IdDemandante",
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
                name: "IX_RegistroServico_IdDemanda",
                table: "RegistroServico",
                column: "IdDemanda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demanda_IdDemandante",
                table: "Demanda",
                column: "IdDemandante");

            migrationBuilder.CreateIndex(
                name: "IX_Demanda_PrestadorId",
                table: "Demanda",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demanda_AspNetUsers_IdDemandante",
                table: "Demanda",
                column: "IdDemandante",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Demanda_AspNetUsers_PrestadorId",
                table: "Demanda",
                column: "PrestadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
