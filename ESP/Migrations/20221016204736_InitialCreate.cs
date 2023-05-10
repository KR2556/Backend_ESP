using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProhibitionCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProhibitionCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProhibitionCodes_CheckCodes_CheckCodeId",
                        column: x => x.CheckCodeId,
                        principalTable: "CheckCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_SubjectTypes_SubjectTypeId",
                        column: x => x.SubjectTypeId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckBlocks_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CheckBlocksAndCheckCodes",
                columns: table => new
                {
                    CheckBlocksId = table.Column<int>(type: "int", nullable: false),
                    CheckCodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBlocksAndCheckCodes", x => new { x.CheckBlocksId, x.CheckCodesId });
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndCheckCodes_CheckBlocks_CheckBlocksId",
                        column: x => x.CheckBlocksId,
                        principalTable: "CheckBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndCheckCodes_CheckCodes_CheckCodesId",
                        column: x => x.CheckCodesId,
                        principalTable: "CheckCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckBlocksAndSubjectTypes",
                columns: table => new
                {
                    CheckBlocksId = table.Column<int>(type: "int", nullable: false),
                    SubjectTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBlocksAndSubjectTypes", x => new { x.CheckBlocksId, x.SubjectTypesId });
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndSubjectTypes_CheckBlocks_CheckBlocksId",
                        column: x => x.CheckBlocksId,
                        principalTable: "CheckBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndSubjectTypes_SubjectTypes_SubjectTypesId",
                        column: x => x.SubjectTypesId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_SubjectTypeId",
                table: "Blocks",
                column: "SubjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBlocks_BlockId",
                table: "CheckBlocks",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBlocksAndCheckCodes_CheckCodesId",
                table: "CheckBlocksAndCheckCodes",
                column: "CheckCodesId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBlocksAndSubjectTypes_SubjectTypesId",
                table: "CheckBlocksAndSubjectTypes",
                column: "SubjectTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProhibitionCodes_CheckCodeId",
                table: "ProhibitionCodes",
                column: "CheckCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckBlocksAndCheckCodes");

            migrationBuilder.DropTable(
                name: "CheckBlocksAndSubjectTypes");

            migrationBuilder.DropTable(
                name: "ProhibitionCodes");

            migrationBuilder.DropTable(
                name: "CheckBlocks");

            migrationBuilder.DropTable(
                name: "CheckCodes");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "SubjectTypes");
        }
    }
}
