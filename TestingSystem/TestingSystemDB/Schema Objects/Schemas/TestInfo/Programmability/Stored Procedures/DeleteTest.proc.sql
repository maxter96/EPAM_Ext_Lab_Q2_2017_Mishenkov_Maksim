CREATE PROCEDURE [TestInfo].[DeleteTest] 
@TestID int
AS
DELETE FROM [TestInfo].[Test] WHERE [TestID] = @TestID;