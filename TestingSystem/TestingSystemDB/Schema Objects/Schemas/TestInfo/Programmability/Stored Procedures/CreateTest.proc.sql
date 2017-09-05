CREATE PROCEDURE [TestInfo].[CreateTest] 
@TestName nvarchar(50),
@TimeLimit int,
@AttemptsCount int
AS
IF NOT EXISTS 
(SELECT [TestID] FROM [TestInfo].[Test] WHERE [TestName] = @TestName)
BEGIN
	INSERT INTO [TestInfo].[Test] ([TestName], [TimeLimit], [AttemptsCount])
	VALUES (@TestName, @TimeLimit, @AttemptsCount);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END