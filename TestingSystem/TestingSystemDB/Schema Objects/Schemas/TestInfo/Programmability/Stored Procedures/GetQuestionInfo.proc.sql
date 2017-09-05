CREATE PROCEDURE [TestInfo].[GetQuestionInfo]
@QuestionID int
AS
SELECT [QuestionID], [QuestionText], [QuestionType], [ThemeID] 
FROM [TestInfo].[Question]
WHERE [QuestionID] = @QuestionID;