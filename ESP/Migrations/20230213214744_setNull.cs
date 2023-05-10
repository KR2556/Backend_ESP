using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class setNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes",
                column: "ClientTypeId",
                principalTable: "ClientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTypes_ClientTypes_ClientTypeId",
                table: "SubjectTypes",
                column: "ClientTypeId",
                principalTable: "ClientTypes",
                principalColumn: "Id");
        }
    }
}
