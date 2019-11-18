using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class updateRecipy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Recipe_TotalBrewTime",
                table: "BrewerTypes",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 1,
                column: "Recipe_TotalBrewTime",
                value: new TimeSpan(0, 0, 3, 0, 0));

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 2,
                column: "Recipe_TotalBrewTime",
                value: new TimeSpan(0, 0, 1, 0, 0));

            migrationBuilder.UpdateData(
                table: "BrewerTypes",
                keyColumn: "BrewerTypeId",
                keyValue: 3,
                column: "Recipe_TotalBrewTime",
                value: new TimeSpan(0, 1, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipe_TotalBrewTime",
                table: "BrewerTypes");
        }
    }
}
