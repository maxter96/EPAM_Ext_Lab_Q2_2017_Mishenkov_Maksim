CREATE PROCEDURE [TestInfo].[DeleteTheme] 
@ThemeID int
AS
IF NOT EXISTS 
(SELECT q.[ThemeID] FROM [TestInfo].[Task] AS t
JOIN [TestInfo].[Question] AS q
ON t.[QuestionID] = q.[QuestionID]
WHERE q.ThemeID = @ThemeID)
BEGIN
	DELETE FROM [TestInfo].[Theme] WHERE [ThemeID] = @ThemeID;
	SELECT @ThemeID;
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END;