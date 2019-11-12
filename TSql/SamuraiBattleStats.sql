USE [BethanysPieShop-1ED06986-5F07-4A1C-85B9-D9F3F477BFF5]
GO

CREATE VIEW dbo.SamuraiBattleStats

AS

SELECT dbo.Samurais.Name, dbo.SamuraiBattle.SamuraiId, COUNT(dbo.SamuraiBattle.BattleId) AS NumberOfBattles,
	dbo.EarliestBattleFoughtBySamurai(MIN(dbo.Samurais.Id)) AS EarliestBattle
FROM dbo.SamuraiBattle INNER JOIN
	dbo.Samurais ON dbo.SamuraiBattle.SamuraiId = dbo.Samurais.Id
GROUP BY dbo.Samurais.Name, dbo.SamuraiBattle.SamuraiId