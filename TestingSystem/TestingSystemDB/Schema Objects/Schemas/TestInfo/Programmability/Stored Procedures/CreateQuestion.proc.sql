CREATE PROCEDURE [TestInfo].[CreateQuestion] 
@ThemeID int,
@QuestionText nvarchar(200),
@QuestionType int
AS
IF EXISTS (SELECT [ThemeID] FROM [TestInfo].[Theme] WHERE [ThemeID] = @ThemeID)
AND NOT EXISTS (SELECT [QuestionID] FROM [TestInfo].[Question] WHERE [ThemeID] = @ThemeID AND [QuestionText] = @QuestionText)
BEGIN
	INSERT INTO [TestInfo].[Question] ([ThemeID], [QuestionText], [QuestionType])
	VALUES (@ThemeID, @QuestionText, @QuestionType);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END