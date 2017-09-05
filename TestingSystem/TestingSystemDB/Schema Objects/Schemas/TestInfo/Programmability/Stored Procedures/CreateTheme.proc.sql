CREATE PROCEDURE [TestInfo].[CreateTheme] 
@ThemeName nvarchar(50)
AS
IF NOT EXISTS 
(SELECT [ThemeID] FROM [TestInfo].[Theme] WHERE [ThemeName] = @ThemeName)
BEGIN
	INSERT INTO [TestInfo].[Theme] ([ThemeName])
	VALUES (@ThemeName);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END