CREATE PROCEDURE [TestInfo].[GetThemeQuestions]
@ThemeID int
AS
SELECT [QuestionID], [QuestionText], [QuestionType], [ThemeID]
FROM [TestInfo].[Question]
WHERE [ThemeID] = @ThemeID
ORDER BY [QuestionText];