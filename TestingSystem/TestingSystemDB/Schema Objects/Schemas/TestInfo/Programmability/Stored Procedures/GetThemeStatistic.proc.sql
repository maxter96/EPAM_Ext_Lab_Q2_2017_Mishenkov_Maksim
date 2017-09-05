CREATE PROCEDURE [TestInfo].[GetThemeStatistic]
@TestSessionID int
AS
SELECT [ThemeStatisticID]
, [TestSessionID]
, [ThemeID]
, [RightAnswers]
, [QuestionsCount]
FROM [TestInfo].[ThemeStatistic]
WHERE [TestSessionID] = @TestSessionID;