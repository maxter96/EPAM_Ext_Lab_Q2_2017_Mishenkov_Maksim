CREATE FUNCTION [TestInfo].[GetQuestionPoints] (@TestSessionID int, @QuestionID int)
RETURNS int
BEGIN
	DECLARE @RightAnswersCount int = 0;
	SET @RightAnswersCount = 
		(SELECT [QuestionType] 
		FROM [TestInfo].[Question]
		WHERE [QuestionID] = @QuestionID);
		
	DECLARE @UserRightAnswers int = 0;
	SET @UserRightAnswers = 
	(SELECT COUNT(ua.[AnswerID]) FROM [TestInfo].[Answer] AS a
	JOIN [TestInfo].[UserAnswer] AS ua
	ON a.[AnswerID] = ua.[AnswerID]
	WHERE ua.[TestSessionID] = @TestSessionID
	AND a.[QuestionID] = @QuestionID
	AND a.[IsCorrect] = 1);

	DECLARE @AllUserAnswers int = 0;
	SET @AllUserAnswers = 
	(SELECT COUNT(ua.[AnswerID]) FROM [TestInfo].[Answer] AS a
	JOIN [TestInfo].[UserAnswer] AS ua
	ON a.[AnswerID] = ua.[AnswerID]
	WHERE ua.[TestSessionID] = @TestSessionID
	AND a.[QuestionID] = @QuestionID);

	IF (@RightAnswersCount = @UserRightAnswers AND @UserRightAnswers = @AllUserAnswers)
	BEGIN
		RETURN 1;
	END

	RETURN 0;
END