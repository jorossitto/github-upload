using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class ownedTypeRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Recipe_BrewMinutes",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recipe_Description",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipe_GrindOunces",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipe_GrindSize",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipe_WaterOunces",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipe_WaterTemperatureF",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 1,
                columns: new[] { "Recipe_BrewMinutes", "Recipe_Description", "Recipe_GrindOunces", "Recipe_GrindSize", "Recipe_WaterOunces", "Recipe_WaterTemperatureF" },
                values: new object[] { 3, "So good!", 2, 2, 9, 130 });

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 2,
                columns: new[] { "Recipe_BrewMinutes", "Recipe_Description", "Recipe_GrindOunces", "Recipe_GrindSize", "Recipe_WaterOunces", "Recipe_WaterTemperatureF" },
                values: new object[] { 1, "Love a hand pressed coffee!", 2, 2, 9, 130 });

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 3,
                columns: new[] { "Recipe_BrewMinutes", "Recipe_Description", "Recipe_GrindOunces", "Recipe_GrindSize", "Recipe_WaterOunces", "Recipe_WaterTemperatureF" },
                values: new object[] { 60, "Cold brew is worth the wait!", 2, 2, 9, 130 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipe_BrewMinutes",
                table: "BrewerTypes");

            migrationBuilder.DropColumn(
                name: "Recipe_Description",
                table: "BrewerTypes");

            migrationBuilder.DropColumn(
                name: "Recipe_GrindOunces",
                table: "BrewerTypes");

            migrationBuilder.DropColumn(
                name: "Recipe_GrindSize",
                table: "BrewerTypes");

            migrationBuilder.DropColumn(
                name: "Recipe_WaterOunces",
                table: "BrewerTypes");

            migrationBuilder.DropColumn(
                name: "Recipe_WaterTemperatureF",
                table: "BrewerTypes");
        }
    }
}
