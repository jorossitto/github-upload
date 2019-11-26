using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class TalksToSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Talks",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abstract",
                table: "Talks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresenterId",
                table: "Talks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Session",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Presenters",
                columns: table => new
                {
                    PresenterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presenters", x => x.PresenterId);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TalkId = table.Column<int>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submission_Conference_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conference",
                        principalColumn: "ConferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submission_Talks_TalkId",
                        column: x => x.TalkId,
                        principalTable: "Talks",
                        principalColumn: "TalkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Presenters",
                columns: new[] { "PresenterId", "Created", "LastModified" },
                values: new object[] { 1, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 1,
                column: "PresenterId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Talks",
                keyColumn: "TalkId",
                keyValue: 2,
                column: "PresenterId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Talks_PresenterId",
                table: "Talks",
                column: "PresenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_ConferenceId",
                table: "Submission",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_TalkId",
                table: "Submission",
                column: "TalkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Talks_Presenters_PresenterId",
                table: "Talks",
                column: "PresenterId",
                principalTable: "Presenters",
                principalColumn: "PresenterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talks_Presenters_PresenterId",
                table: "Talks");

            migrationBuilder.DropTable(
                name: "Presenters");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Talks_PresenterId",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "PresenterId",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Session");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Talks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Abstract",
                table: "Talks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
