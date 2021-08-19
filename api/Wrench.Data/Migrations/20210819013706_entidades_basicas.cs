using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class entidades_basicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    IdChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.IdChat);
                });

            migrationBuilder.CreateTable(
                name: "Demanda",
                columns: table => new
                {
                    IdDemanda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDemandante = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demanda", x => x.IdDemanda);
                    table.ForeignKey(
                        name: "FK_Demanda_AspNetUsers_IdDemandante",
                        column: x => x.IdDemandante,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    IdTag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.IdTag);
                });

            migrationBuilder.CreateTable(
                name: "ChatConversa",
                columns: table => new
                {
                    IdChatConversa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChat = table.Column<int>(type: "int", nullable: false),
                    De = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Para = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnviadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConversa", x => x.IdChatConversa);
                    table.ForeignKey(
                        name: "FK_ChatConversa_AspNetUsers_De",
                        column: x => x.De,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatConversa_AspNetUsers_Para",
                        column: x => x.Para,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatConversa_Chat_IdChat",
                        column: x => x.IdChat,
                        principalTable: "Chat",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroServico",
                columns: table => new
                {
                    IdRegistroServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    IdDemanda = table.Column<int>(type: "int", nullable: false),
                    ValorEstimado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorCobrado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroServico", x => x.IdRegistroServico);
                    table.ForeignKey(
                        name: "FK_RegistroServico_Demanda_IdDemanda",
                        column: x => x.IdDemanda,
                        principalTable: "Demanda",
                        principalColumn: "IdDemanda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    IdAvaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRegistroServico = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorNota = table.Column<int>(type: "int", nullable: false),
                    EnviadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.IdAvaliacao);
                    table.ForeignKey(
                        name: "FK_Avaliacao_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_RegistroServico_IdRegistroServico",
                        column: x => x.IdRegistroServico,
                        principalTable: "RegistroServico",
                        principalColumn: "IdRegistroServico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IdRegistroServico",
                table: "Avaliacao",
                column: "IdRegistroServico");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IdUsuario",
                table: "Avaliacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversa_De",
                table: "ChatConversa",
                column: "De");

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversa_IdChat",
                table: "ChatConversa",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversa_Para",
                table: "ChatConversa",
                column: "Para");

            migrationBuilder.CreateIndex(
                name: "IX_Demanda_IdDemandante",
                table: "Demanda",
                column: "IdDemandante");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroServico_IdDemanda",
                table: "RegistroServico",
                column: "IdDemanda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Nome",
                table: "Tag",
                column: "Nome",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "ChatConversa");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "RegistroServico");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Demanda");
        }
    }
}
