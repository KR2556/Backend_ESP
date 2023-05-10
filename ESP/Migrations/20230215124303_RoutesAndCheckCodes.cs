using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class RoutesAndCheckCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutesAndCheckCodes",
                columns: table => new
                {
                    CheckCodesId = table.Column<int>(type: "int", nullable: false),
                    RoutesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesAndCheckCodes", x => new { x.CheckCodesId, x.RoutesId });
                    table.ForeignKey(
                        name: "FK_RoutesAndCheckCodes_CheckCodes_CheckCodesId",
                        column: x => x.CheckCodesId,
                        principalTable: "CheckCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutesAndCheckCodes_Routes_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutesAndCheckCodes_RoutesId",
                table: "RoutesAndCheckCodes",
                column: "RoutesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutesAndCheckCodes");
        }
    }
}
