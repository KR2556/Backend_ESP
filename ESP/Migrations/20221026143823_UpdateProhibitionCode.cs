using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class UpdateProhibitionCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_SubjectTypes_SubjectTypeId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_SubjectTypeId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "SubjectTypeId",
                table: "Blocks");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ProhibitionCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProhibitionCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProhibitionCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProhibitionCodes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProhibitionCodes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProhibitionCodes");

            migrationBuilder.AddColumn<int>(
                name: "SubjectTypeId",
                table: "Blocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_SubjectTypeId",
                table: "Blocks",
                column: "SubjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_SubjectTypes_SubjectTypeId",
                table: "Blocks",
                column: "SubjectTypeId",
                principalTable: "SubjectTypes",
                principalColumn: "Id");
        }
    }
}
