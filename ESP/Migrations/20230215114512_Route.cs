using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasPin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutesAndProhibitionCodes",
                columns: table => new
                {
                    ProhibitionCodesId = table.Column<int>(type: "int", nullable: false),
                    RoutesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesAndProhibitionCodes", x => new { x.ProhibitionCodesId, x.RoutesId });
                    table.ForeignKey(
                        name: "FK_RoutesAndProhibitionCodes_ProhibitionCodes_ProhibitionCodesId",
                        column: x => x.ProhibitionCodesId,
                        principalTable: "ProhibitionCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutesAndProhibitionCodes_Routes_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutesAndProhibitionCodes_RoutesId",
                table: "RoutesAndProhibitionCodes",
                column: "RoutesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutesAndProhibitionCodes");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
