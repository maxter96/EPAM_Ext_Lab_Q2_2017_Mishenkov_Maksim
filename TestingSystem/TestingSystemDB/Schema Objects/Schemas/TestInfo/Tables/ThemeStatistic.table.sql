CREATE TABLE [TestInfo].[ThemeStatistic]
(
	[ThemeStatisticID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TestSessionID] INT NOT NULL, 
    [ThemeID] INT NOT NULL, 
    [QuestionsCount] INT NOT NULL DEFAULT 0, 
    [RightAnswers] INT NOT NULL DEFAULT 0
)
