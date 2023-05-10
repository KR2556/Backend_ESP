using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class RemoveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Processes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
