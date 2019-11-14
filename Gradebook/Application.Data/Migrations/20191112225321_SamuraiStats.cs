using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCore.Data.Migrations
{
    public partial class SamuraiStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
                (@"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
                    AS
                    Select sb.SamuraiId, s.Name, COUNT(sb.BattleId) AS NumberOfBattles, dbo.EarliestBattleFoughtBySamurai(MIN(s.Id)) AS EarliestBattle
                    FROM dbo.SamuraiBattle sb INNER JOIN 
	                    dbo.Samurais s ON s.Id = sb.SamuraiId
                    GROUP BY s.Name, sb.SamuraiId"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.SamuraiBattleStats");
        }
    }
}
