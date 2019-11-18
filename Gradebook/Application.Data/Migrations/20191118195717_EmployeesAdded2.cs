using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class EmployeesAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "CloseTime",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { "5pm", "5am" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { "6pm", "6am" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { "7pm", "7am" });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                columns: new[] { "BrewerTypeId", "LocationId" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Acquired", "BrewerTypeId", "LocationId" },
                values: new object[,]
                {
                    { 3, new DateTime(2018, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 4, new DateTime(2018, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "OpenTime",
                table: "Locations",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CloseTime",
                table: "Locations",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "LocationId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                columns: new[] { "BrewerTypeId", "LocationId" },
                values: new object[] { 1, null });
        }
    }
}
