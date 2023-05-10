using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class renameSystemBlockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_systemBlocks",
                table: "systemBlocks");

            migrationBuilder.RenameTable(
                name: "systemBlocks",
                newName: "SystemBlocks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemBlocks",
                table: "SystemBlocks",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemBlocks",
                table: "SystemBlocks");

            migrationBuilder.RenameTable(
                name: "SystemBlocks",
                newName: "systemBlocks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_systemBlocks",
                table: "systemBlocks",
                column: "Id");
        }
    }
}
