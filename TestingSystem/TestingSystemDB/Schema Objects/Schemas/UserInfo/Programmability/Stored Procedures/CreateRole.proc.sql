CREATE PROCEDURE [UserInfo].[CreateRole] 
@RoleName nvarchar(10)
AS
IF NOT EXISTS
(SELECT [RoleID] FROM [UserInfo].[Role] WHERE [RoleName] = @RoleName)
BEGIN
	INSERT INTO [UserInfo].[Role] VALUES (@RoleName);
	SELECT 1;
	RETURN;
END
ELSE
BEGIN
	SELECT 0;
	RETURN;
END