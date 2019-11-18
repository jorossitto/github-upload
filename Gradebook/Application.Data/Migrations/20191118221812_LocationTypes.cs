using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class LocationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationType",
                table: "Locations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "LocationType",
                value: "Kiosk");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                column: "LocationType",
                value: "FoodTruck");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationType",
                table: "Locations");
        }
    }
}
