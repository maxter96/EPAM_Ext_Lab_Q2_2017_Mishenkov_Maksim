CREATE PROCEDURE [TestInfo].[DeleteQuestion] 
@QuestionID int
AS
IF NOT EXISTS (SELECT [QuestionID] FROM [TestInfo].[Task] WHERE [QuestionID] = @QuestionID)
BEGIN
	DELETE FROM [TestInfo].[Question] WHERE [QuestionID] = @QuestionID;
	SELECT 1;
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END;