-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Dbo].[EarliestBattleFoughtBySamurai] (@samuraiId INT)
RETURNS CHAR(30) AS
BEGIN
	DECLARE @ret CHAR(30);
	SELECT TOP 1 @ret = NAME
	FROM Battles AS b
	WHERE b.Id IN (SELECT sb.BattleId
	               FROM SamuraiBattle AS sb
	               WHERE sb.SamuraiId = @samuraiId
					)
	ORDER BY b.StartDate
	RETURN @ret;
	END

