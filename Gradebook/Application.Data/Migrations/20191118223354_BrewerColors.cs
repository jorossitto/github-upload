using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class BrewerColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BrewerTypes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "BrewerTypes");
        }
    }
}
