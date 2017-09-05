CREATE PROCEDURE [TestInfo].[CreateTestSession] 
@TestID int,
@UserID int
AS
IF EXISTS 
(SELECT [UserID] FROM [UserInfo].[User] WHERE [UserID] = @UserID)
AND EXISTS
(SELECT [TestID] FROM [TestInfo].[Test] WHERE [TestID] = @TestID)
BEGIN
	IF EXISTS 
	(SELECT[TestSessionID] 
	FROM [TestInfo].[TestSession]
	WHERE [UserID] = @UserID
	AND [TestID] = @TestID
	AND [Time] IS NULL)
	BEGIN
		SELECT MAX([TestSessionID]) FROM [TestInfo].[TestSession]
		WHERE [UserID] = @UserID
		AND [TestID] = @TestID
		AND [Time] IS NULL
		RETURN;
	END
	ELSE
	BEGIN
		IF 
		(
			(SELECT COUNT([TestSessionID]) 
			FROM [TestInfo].[TestSession]
			WHERE [TestID] = @TestID AND [UserID] = @UserID)
			<
			[TestInfo].[GetAttemptsCount] (@TestID)
		)
		BEGIN
			INSERT INTO [TestInfo].[TestSession] ([TestID], [UserID], [BeginTime], [EndTime], [QuestionsCount])
			VALUES (
				@TestID
				, @UserID
				, GETDATE()
				, DATEADD(minute, (SELECT [TimeLimit] FROM [TestInfo].[Test] WHERE [TestID] = @TestID), GETDATE())
				, (SELECT COUNT([TaskID]) FROM [TestInfo].[Task]
				WHERE [TestID] = @TestID)
			);
			SELECT CONVERT(int, SCOPE_IDENTITY());
			RETURN;
		END
	END
END
SELECT -1;
RETURN;