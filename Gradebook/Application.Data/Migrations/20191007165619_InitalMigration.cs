using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Pies",
                columns: table => new
                {
                    PieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityState = table.Column<int>(nullable: false),
                    HasChanges = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    CurrentPrice = table.Column<decimal>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    InStock = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ListPrice = table.Column<float>(nullable: false),
                    AllergyInformation = table.Column<string>(nullable: true),
                    IsPieOfTheWeek = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pies", x => x.PieId);
                    table.ForeignKey(
                        name: "FK_Pies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pies_CategoryId",
                table: "Pies",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pies");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
