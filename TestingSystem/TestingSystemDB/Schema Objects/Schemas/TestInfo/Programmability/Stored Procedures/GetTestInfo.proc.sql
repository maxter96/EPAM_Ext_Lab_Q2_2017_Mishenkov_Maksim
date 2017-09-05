CREATE PROC [TestInfo].[GetTestInfo]
@TestID int
AS
SELECT [TestID], [TestName], [TimeLimit], [AttemptsCount]
FROM [TestInfo].[Test]
WHERE [TestID] = @TestID;
