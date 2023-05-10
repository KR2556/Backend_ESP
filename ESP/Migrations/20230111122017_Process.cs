using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Process : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessesAndCheckBlocks",
                columns: table => new
                {
                    CheckBlocksId = table.Column<int>(type: "int", nullable: false),
                    ProcessesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesAndCheckBlocks", x => new { x.CheckBlocksId, x.ProcessesId });
                    table.ForeignKey(
                        name: "FK_ProcessesAndCheckBlocks_CheckBlocks_CheckBlocksId",
                        column: x => x.CheckBlocksId,
                        principalTable: "CheckBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesAndCheckBlocks_Processes_ProcessesId",
                        column: x => x.ProcessesId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessesAndCheckCodes",
                columns: table => new
                {
                    CheckCodesId = table.Column<int>(type: "int", nullable: false),
                    ProcessesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesAndCheckCodes", x => new { x.CheckCodesId, x.ProcessesId });
                    table.ForeignKey(
                        name: "FK_ProcessesAndCheckCodes_CheckCodes_CheckCodesId",
                        column: x => x.CheckCodesId,
                        principalTable: "CheckCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesAndCheckCodes_Processes_ProcessesId",
                        column: x => x.ProcessesId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessesAndProhibitionCodes",
                columns: table => new
                {
                    ProcessesId = table.Column<int>(type: "int", nullable: false),
                    ProhibitionCodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesAndProhibitionCodes", x => new { x.ProcessesId, x.ProhibitionCodesId });
                    table.ForeignKey(
                        name: "FK_ProcessesAndProhibitionCodes_Processes_ProcessesId",
                        column: x => x.ProcessesId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesAndProhibitionCodes_ProhibitionCodes_ProhibitionCodesId",
                        column: x => x.ProhibitionCodesId,
                        principalTable: "ProhibitionCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessesAndSubjectTypes",
                columns: table => new
                {
                    ProcessesId = table.Column<int>(type: "int", nullable: false),
                    SubjectTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesAndSubjectTypes", x => new { x.ProcessesId, x.SubjectTypesId });
                    table.ForeignKey(
                        name: "FK_ProcessesAndSubjectTypes_Processes_ProcessesId",
                        column: x => x.ProcessesId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesAndSubjectTypes_SubjectTypes_SubjectTypesId",
                        column: x => x.SubjectTypesId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesAndCheckBlocks_ProcessesId",
                table: "ProcessesAndCheckBlocks",
                column: "ProcessesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesAndCheckCodes_ProcessesId",
                table: "ProcessesAndCheckCodes",
                column: "ProcessesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesAndProhibitionCodes_ProhibitionCodesId",
                table: "ProcessesAndProhibitionCodes",
                column: "ProhibitionCodesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesAndSubjectTypes_SubjectTypesId",
                table: "ProcessesAndSubjectTypes",
                column: "SubjectTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessesAndCheckBlocks");

            migrationBuilder.DropTable(
                name: "ProcessesAndCheckCodes");

            migrationBuilder.DropTable(
                name: "ProcessesAndProhibitionCodes");

            migrationBuilder.DropTable(
                name: "ProcessesAndSubjectTypes");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
