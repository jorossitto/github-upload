using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class newLocationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                column: "LocationType",
                value: "Storefront");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                column: "LocationType",
                value: "Popup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                column: "LocationType",
                value: "FoodTruck");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                column: "LocationType",
                value: "Storefront");
        }
    }
}
