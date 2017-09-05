CREATE PROCEDURE [TestInfo].[GetThemeInfo]
@ThemeID int
AS
SELECT t.[ThemeID], 
t.[ThemeName],
(SELECT COUNT(q.[QuestionID])
FROM [TestInfo].[Question] AS q
WHERE q.[ThemeID] = t.[ThemeID]) AS [QuestionsCount]
FROM [TestInfo].[Theme] AS t
WHERE t.[ThemeID] = @ThemeID;