CREATE PROCEDURE [TestInfo].[GetUserTestSessions]
@TestID int,
@UserID int
AS
SELECT [TestSessionID]
, [TestID]
, [UserID]
, [BeginTime]
, [EndTime]
, [Time] 
, [RightAnswers]
, [QuestionsCount]
FROM [TestInfo].[TestSession]
WHERE [TestID] = @TestID
AND [UserID] = @UserID;