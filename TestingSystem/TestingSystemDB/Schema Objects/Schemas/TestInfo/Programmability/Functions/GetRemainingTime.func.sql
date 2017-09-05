CREATE FUNCTION [TestInfo].[GetRemainingTime] (@TestSessionID int)
RETURNS int
BEGIN
	DECLARE @difference bit = 0;
	DECLARE @remainingTime int = 0;

	SELECT @difference = IIF([EndTime] > GETDATE(), 1, 0),
	@remainingTime = DATEDIFF(minute, GETDATE(), [EndTime])
	FROM [TestInfo].[TestSession]
	WHERE [TestSessionID] = @TestSessionID;

	IF (@difference = 1)
	BEGIN
		RETURN @remainingTime;
	END
	RETURN 0;
END