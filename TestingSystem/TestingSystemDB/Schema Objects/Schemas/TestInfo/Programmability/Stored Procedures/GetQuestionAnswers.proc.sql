CREATE PROCEDURE [TestInfo].[GetQuestionAnswers] 
@QuestionID int
AS
SELECT [AnswerID], [QuestionID], [AnswerText], [IsCorrect]
FROM [TestInfo].[Answer]
WHERE [QuestionID] = @QuestionID;