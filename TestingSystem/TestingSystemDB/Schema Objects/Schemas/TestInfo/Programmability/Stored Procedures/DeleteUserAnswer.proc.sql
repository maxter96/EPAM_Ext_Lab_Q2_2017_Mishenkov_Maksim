CREATE PROCEDURE [TestInfo].[DeleteUserAnswer]
@TestSessionID int,
@AnswerID int
AS
DELETE FROM [TestInfo].[UserAnswer]
WHERE [TestSessionID] = @TestSessionID
AND [AnswerID] = @AnswerID;