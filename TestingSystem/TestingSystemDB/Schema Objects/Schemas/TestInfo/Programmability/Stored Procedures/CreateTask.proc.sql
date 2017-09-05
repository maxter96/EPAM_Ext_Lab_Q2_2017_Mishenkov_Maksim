CREATE PROCEDURE [TestInfo].[CreateTask] 
@TestID int,
@QuestionID int
AS
IF EXISTS 
(SELECT [TestID] FROM [TestInfo].[Test] WHERE [TestID] = @TestID)
AND EXISTS
(SELECT [QuestionID] FROM [TestInfo].[Question] WHERE [QuestionID] = @QuestionID)
BEGIN
	INSERT INTO [TestInfo].[Task] ([TestID], [QuestionID])
	VALUES (@TestID, @QuestionID);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END