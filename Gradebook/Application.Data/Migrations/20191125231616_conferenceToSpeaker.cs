using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class conferenceToSpeaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course)_Author_AuthorId",
                table: "Course)");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course)",
                table: "Course)");

            migrationBuilder.RenameTable(
                name: "Course)",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_Course)_AuthorId",
                table: "Course",
                newName: "IX_Course_AuthorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Units",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Units",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Talks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Talks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ConferenceId",
                table: "Speakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Conference",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "Conference",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Conference",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Speakers",
                keyColumn: "SpeakerId",
                keyValue: 1,
                column: "ConferenceId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Speakers",
                keyColumn: "SpeakerId",
                keyValue: 2,
                column: "ConferenceId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                columns: new[] { "Cost", "Created", "LastModified" },
                values: new object[] { 99m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                columns: new[] { "Cost", "Created", "LastModified" },
                values: new object[] { 99m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3,
                columns: new[] { "Cost", "Created", "LastModified" },
                values: new object[] { 99m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4,
                columns: new[] { "Cost", "Created", "LastModified" },
                values: new object[] { 99m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_ConferenceId",
                table: "Speakers",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Author_AuthorId",
                table: "Course",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Conference_ConferenceId",
                table: "Speakers",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "ConferenceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Author_AuthorId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Conference_ConferenceId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_ConferenceId",
                table: "Speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Conference");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "Conference");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Conference");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Course)");

            migrationBuilder.RenameIndex(
                name: "IX_Course_AuthorId",
                table: "Course)",
                newName: "IX_Course)_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course)",
                table: "Course)",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "Cost",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "Cost",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "Cost",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "Cost",
                value: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Course)_Author_AuthorId",
                table: "Course)",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
