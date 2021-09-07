using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class tags_usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagUsers",
                columns: table => new
                {
                    AtribuidosParaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsIdTag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagUsers", x => new { x.AtribuidosParaId, x.TagsIdTag });
                    table.ForeignKey(
                        name: "FK_TagUsers_AspNetUsers_AtribuidosParaId",
                        column: x => x.AtribuidosParaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagUsers_Tag_TagsIdTag",
                        column: x => x.TagsIdTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_TagsIdTag",
                table: "TagUsers",
                column: "TagsIdTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagUsers");
        }
    }
}
