CREATE TABLE [TestInfo].[TestSession]
(
	[TestSessionID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] INT NOT NULL, 
    [TestID] INT NOT NULL, 
    [BeginTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [Time] DATETIME NULL, 
    [QuestionsCount] INT NOT NULL DEFAULT 0, 
    [RightAnswers] INT NOT NULL DEFAULT 0
)
