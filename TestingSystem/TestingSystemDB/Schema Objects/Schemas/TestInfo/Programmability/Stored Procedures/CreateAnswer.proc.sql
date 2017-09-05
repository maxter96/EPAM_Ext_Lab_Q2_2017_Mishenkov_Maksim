CREATE PROCEDURE [TestInfo].[CreateAnswer] 
@QuestionID int,
@AnswerText nvarchar(200),
@IsCorrect int
AS
IF EXISTS 
(SELECT [QuestionID] FROM [TestInfo].[Question] WHERE [QuestionID] = @QuestionID)
BEGIN
	INSERT INTO [TestInfo].[Answer] ([QuestionID], [AnswerText], [IsCorrect])
	VALUES (@QuestionID, @AnswerText, @IsCorrect);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END