using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    EntityState = table.Column<int>(nullable: false),
                    HasChanges = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressType = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    StreetLine1 = table.Column<string>(nullable: true),
                    StreetLine2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    EntityState = table.Column<int>(nullable: false),
                    HasChanges = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    Cuisine = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Restaurants_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
