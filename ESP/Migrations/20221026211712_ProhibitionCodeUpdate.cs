using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class ProhibitionCodeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProhibitionCodes_CheckCodes_CheckCodeId",
                table: "ProhibitionCodes");

            migrationBuilder.AlterColumn<int>(
                name: "CheckCodeId",
                table: "ProhibitionCodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProhibitionCodes_CheckCodes_CheckCodeId",
                table: "ProhibitionCodes",
                column: "CheckCodeId",
                principalTable: "CheckCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProhibitionCodes_CheckCodes_CheckCodeId",
                table: "ProhibitionCodes");

            migrationBuilder.AlterColumn<int>(
                name: "CheckCodeId",
                table: "ProhibitionCodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProhibitionCodes_CheckCodes_CheckCodeId",
                table: "ProhibitionCodes",
                column: "CheckCodeId",
                principalTable: "CheckCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
