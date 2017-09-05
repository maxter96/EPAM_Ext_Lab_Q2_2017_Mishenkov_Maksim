CREATE PROCEDURE [TestInfo].[GetTestSessionInfo]
@TestSessionID int
AS
SELECT 
[TestSessionID],
[UserID], 
[TestID], 
[BeginTime], 
[EndTime], 
[Time], 
[QuestionsCount], 
[RightAnswers]
FROM [TestInfo].[TestSession]
WHERE [TestSessionID] = @TestSessionID;