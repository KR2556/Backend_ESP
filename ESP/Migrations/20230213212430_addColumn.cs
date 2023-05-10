using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
