CREATE PROCEDURE [TestInfo].[CreateUserAnswer] 
@TestSessionID int,
@AnswerID int
AS

IF EXISTS 
(SELECT [TestSessionID] 
FROM [TestInfo].[TestSession] 
WHERE [TestSessionID] = @TestSessionID
AND [EndTime] > GETDATE()
AND [Time] IS NULL)

AND EXISTS
(SELECT [AnswerID]
FROM [TestInfo].[Answer]
WHERE [AnswerID] = @AnswerID)

BEGIN
	IF NOT EXISTS 
	(SELECT [AnswerID] 
	FROM [TestInfo].[UserAnswer]
	WHERE [TestSessionID] = @TestSessionID
	AND [AnswerID] = @AnswerID)
	BEGIN
		INSERT INTO [TestInfo].[UserAnswer] ([TestSessionID], [AnswerID])
		VALUES (@TestSessionID, @AnswerID);
		SELECT CONVERT(int, SCOPE_IDENTITY())
		RETURN;
	END
	ELSE
	BEGIN
		SELECT -1;
		RETURN;
	END
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END