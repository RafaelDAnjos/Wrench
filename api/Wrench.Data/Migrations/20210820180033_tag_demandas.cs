using Microsoft.EntityFrameworkCore.Migrations;

namespace Wrench.Data.Migrations
{
    public partial class tag_demandas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagDemandas",
                columns: table => new
                {
                    DemandasIdDemanda = table.Column<int>(type: "int", nullable: false),
                    TagsIdTag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDemandas", x => new { x.DemandasIdDemanda, x.TagsIdTag });
                    table.ForeignKey(
                        name: "FK_TagDemandas_Demanda_DemandasIdDemanda",
                        column: x => x.DemandasIdDemanda,
                        principalTable: "Demanda",
                        principalColumn: "IdDemanda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagDemandas_Tag_TagsIdTag",
                        column: x => x.TagsIdTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagDemandas_TagsIdTag",
                table: "TagDemandas",
                column: "TagsIdTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagDemandas");
        }
    }
}
