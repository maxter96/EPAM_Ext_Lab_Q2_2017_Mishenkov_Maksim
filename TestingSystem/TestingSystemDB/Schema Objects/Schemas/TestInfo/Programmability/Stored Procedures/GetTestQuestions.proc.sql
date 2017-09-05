CREATE PROCEDURE [TestInfo].[GetTestQuestions] 
@TestID int
AS
SELECT q.[QuestionID], q.[QuestionText], q.[QuestionType], q.[ThemeID] 
FROM [TestInfo].[Task] AS t
JOIN [TestInfo].[Question] AS q
ON t.[QuestionID] = q.[QuestionID]
WHERE t.TestID = @TestID;