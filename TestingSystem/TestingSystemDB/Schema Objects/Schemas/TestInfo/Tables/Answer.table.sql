CREATE TABLE [TestInfo].[Answer]
(
	[AnswerID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionID] INT NOT NULL, 
    [AnswerText] NVARCHAR(200) NOT NULL, 
    [IsCorrect] BIT NOT NULL
)
