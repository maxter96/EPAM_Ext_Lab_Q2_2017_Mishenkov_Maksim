CREATE FUNCTION [TestInfo].[GetAttemptsCount] (@TestID int)
RETURNS int
BEGIN
	DECLARE @attemptsCount int;

	IF EXISTS 
	(SELECT [TestID] 
	FROM [TestInfo].[Test]
	WHERE [TestID] = @TestID)
	BEGIN
		SET @attemptsCount = 
		(SELECT [AttemptsCount] 
		FROM [TestInfo].[Test]
		WHERE [TestID] = @TestID);
		RETURN @AttemptsCount;
	END

	SET @attemptsCount = 0;
	RETURN @AttemptsCount;
END