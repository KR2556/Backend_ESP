using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class SubjectTypesAndClientTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTypes_ClientTypeId",
                table: "SubjectTypes");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                table: "SubjectTypes");

            migrationBuilder.CreateTable(
                name: "SubjectTypesAndClientTypes",
                columns: table => new
                {
                    ClientTypesId = table.Column<int>(type: "int", nullable: false),
                    SubjectTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypesAndClientTypes", x => new { x.ClientTypesId, x.SubjectTypesId });
                    table.ForeignKey(
                        name: "FK_SubjectTypesAndClientTypes_ClientTypes_ClientTypesId",
                        column: x => x.ClientTypesId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTypesAndClientTypes_SubjectTypes_SubjectTypesId",
                        column: x => x.SubjectTypesId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTypesAndClientTypes_SubjectTypesId",
                table: "SubjectTypesAndClientTypes",
                column: "SubjectTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTypesAndClientTypes");

            migrationBuilder.AddColumn<int>(
                name: "ClientTypeId",
                table: "SubjectTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTypes_ClientTypeId",
                table: "SubjectTypes",
                column: "ClientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes",
                column: "ClientTypeId",
                principalTable: "ClientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
