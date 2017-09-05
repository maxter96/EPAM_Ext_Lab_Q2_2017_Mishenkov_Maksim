CREATE PROCEDURE [TestInfo].[GetUserAnswers]
@TestSessionID int
AS
SELECT [AnswerID] 
FROM [TestInfo].[UserAnswer]
WHERE [TestSessionID] = @TestSessionID;