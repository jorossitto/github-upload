using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class addedReach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReachId",
                table: "Session",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reach",
                columns: table => new
                {
                    ReachId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reach", x => x.ReachId);
                });

            migrationBuilder.InsertData(
                table: "Reach",
                columns: new[] { "ReachId", "Description" },
                values: new object[] { 1, "Keynote" });

            migrationBuilder.InsertData(
                table: "Reach",
                columns: new[] { "ReachId", "Description" },
                values: new object[] { 2, "Breakout" });

            migrationBuilder.InsertData(
                table: "Reach",
                columns: new[] { "ReachId", "Description" },
                values: new object[] { 3, "Open Space" });

            migrationBuilder.CreateIndex(
                name: "IX_Session_ReachId",
                table: "Session",
                column: "ReachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Reach_ReachId",
                table: "Session",
                column: "ReachId",
                principalTable: "Reach",
                principalColumn: "ReachId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Reach_ReachId",
                table: "Session");

            migrationBuilder.DropTable(
                name: "Reach");

            migrationBuilder.DropIndex(
                name: "IX_Session_ReachId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "ReachId",
                table: "Session");
        }
    }
}
