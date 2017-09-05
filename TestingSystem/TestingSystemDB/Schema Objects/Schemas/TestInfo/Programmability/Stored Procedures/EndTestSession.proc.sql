CREATE PROCEDURE [TestInfo].[EndTestSession] 
@TestSessionID int
AS
IF EXISTS 
(SELECT [TestSessionID] 
FROM [TestInfo].[TestSession] 
WHERE [TestSessionID] = @TestSessionID
AND [Time] IS NULL)
BEGIN
	DECLARE @TestID int;
	SET @TestID = (SELECT [TestID] 
	FROM [TestInfo].[TestSession]
	WHERE [TestSessionID] = @TestSessionID);

	UPDATE [TestInfo].[TestSession]
	SET [Time] = 
		CASE 
			WHEN GETDATE() > [EndTime]
			THEN [EndTime]
			ELSE GETDATE()
		END,
	[RightAnswers] = 
		(SELECT SUM([TestInfo].[GetQuestionPoints] (@TestSessionID, q.[QuestionID]))
		FROM [TestInfo].[Task] AS t
		JOIN [TestInfo].[Question] AS q
		ON t.[QuestionID] = q.[QuestionID]
		WHERE t.[TestID] = @TestID)
	WHERE [TestSessionID] = @TestSessionID;

	INSERT INTO [TestInfo].[ThemeStatistic] ([TestSessionID], [ThemeID], [RightAnswers], [QuestionsCount])
	SELECT @TestSessionID
	, th.[ThemeID] AS [ThemeID]
	, SUM([TestInfo].[GetQuestionPoints] (@TestSessionID, q.[QuestionID]))
	, COUNT(q.[QuestionID])
	FROM [TestInfo].[Task] AS t
	JOIN [TestInfo].[Question] AS q
	ON t.[QuestionID] = q.[QuestionID]
	JOIN [TestInfo].[Theme] AS th
	ON th.[ThemeID] = q.[ThemeID]
	WHERE t.[TestID] = @TestID
	GROUP BY th.[ThemeID];

	SELECT @TestSessionID;
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END
