using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class brewerUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Locations_LocationId",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_LocationId",
                table: "Units",
                newName: "IX_Units_LocationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Acquired",
                table: "Units",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BrewerTypeId",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "UnitId");

            migrationBuilder.CreateTable(
                name: "BrewerTypes",
                columns: table => new
                {
                    BrewerTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrewerTypes", x => x.BrewerTypeId);
                });

            migrationBuilder.InsertData(
                table: "BrewerTypes",
                columns: new[] { "BrewerTypeId", "Description" },
                values: new object[,]
                {
                    { 1, "Glass Hourglass Drip" },
                    { 2, "Hand Press" },
                    { 3, "Cold Brew" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Address1", "Address2", "Address3", "CityTown", "CloseTime", "Country", "OpenTime", "PostalCode", "StateProvince", "VenueName" },
                values: new object[] { 2, "999 Main Street", null, null, "Atlanta", "4PM", "USA", "6AM", "12345", "GA", "Atlanta Convention Center" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Acquired", "BrewerTypeId", "LocationId" },
                values: new object[] { 2, new DateTime(2018, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Acquired", "BrewerTypeId", "LocationId" },
                values: new object[] { 1, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "BrewerTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Acquired",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "BrewerTypeId",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameIndex(
                name: "IX_Units_LocationId",
                table: "Unit",
                newName: "IX_Unit_LocationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Unit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Unit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Locations_LocationId",
                table: "Unit",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
