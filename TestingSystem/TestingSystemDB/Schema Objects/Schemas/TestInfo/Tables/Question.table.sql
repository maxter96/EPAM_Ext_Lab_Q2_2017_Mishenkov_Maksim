CREATE TABLE [TestInfo].[Question]
(
	[QuestionID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ThemeID] INT NOT NULL, 
    [QuestionText] NVARCHAR(200) NOT NULL, 
    [QuestionType] INT NOT NULL
)
