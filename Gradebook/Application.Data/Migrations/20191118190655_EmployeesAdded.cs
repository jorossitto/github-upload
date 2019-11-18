using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class EmployeesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    Barista = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Barista", "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, true, 1, "Leia" },
                    { 2, true, 2, "Rey" },
                    { 3, true, 2, "Gamora" }
                });

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

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Address1", "Address2", "Address3", "CityTown", "CloseTime", "Country", "OpenTime", "PostalCode", "StateProvince", "VenueName" },
                values: new object[] { 3, "3 Main", null, null, null, new TimeSpan(0, 18, 0, 0, 0), null, new TimeSpan(0, 8, 0, 0, 0), null, null, null });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Barista", "LocationId", "Name" },
                values: new object[] { 4, true, 3, "Dr. Strange" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Barista", "LocationId", "Name" },
                values: new object[] { 5, false, 3, "Peter Parker" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LocationId",
                table: "Employee",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Acquired",
                table: "Units",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "CloseTime",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { "4PM", "6AM" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                columns: new[] { "CloseTime", "OpenTime" },
                values: new object[] { "4PM", "6AM" });
        }
    }
}
