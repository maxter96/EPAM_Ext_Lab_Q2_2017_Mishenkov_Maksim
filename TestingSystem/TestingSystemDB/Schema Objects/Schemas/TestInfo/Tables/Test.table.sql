CREATE TABLE [TestInfo].[Test]
(
	[TestID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TestName] NVARCHAR(50) NOT NULL, 
    [TimeLimit] INT NOT NULL, 
    [AttemptsCount] INT NOT NULL
)
