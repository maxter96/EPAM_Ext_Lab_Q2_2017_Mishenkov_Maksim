CREATE VIEW [TestInfo].[GetUserTestPoints] 
([UserID], [UserName], [Login], [RightAnswers], [QuestionsCount])
AS
SELECT 
[UserID],
CONCAT([FirstName], ' ', [LastName]) AS [UserName],
[Login],
(SELECT SUM(ra.[BestScore]) FROM
	(SELECT ts1.[TestID], MAX(ts1.[RightAnswers]) AS [BestScore]
	FROM [TestInfo].[TestSession] AS ts1
	WHERE ts1.[UserID] = u.[UserID]
	GROUP BY ts1.[TestID]) AS ra),
(SELECT COUNT([TaskID]) 
FROM [TestInfo].[Task]) AS [QuestionsCount]
FROM [UserInfo].[User] AS u;