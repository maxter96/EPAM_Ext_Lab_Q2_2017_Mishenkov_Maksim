CREATE PROCEDURE [UserInfo].[AddUserToRole] 
@Login nvarchar(20),
@RoleName nvarchar(10)
AS
IF EXISTS 
(SELECT [UserID] FROM [UserInfo].[User] WHERE [Login] = @Login)
AND EXISTS
(SELECT [RoleID] FROM [UserInfo].[Role] WHERE [RoleName] = @RoleName)
AND
NOT EXISTS
(SELECT [RoleName] FROM [UserInfo].[User] AS u
JOIN [UserInfo].[UserInRole] AS ur
ON u.[UserID] = ur.[UserID]
JOIN [UserInfo].[Role] AS r
ON ur.[RoleID] = r.[RoleID]
WHERE u.[Login] = @Login AND r.[RoleName] = @RoleName)
BEGIN
	INSERT INTO [UserInfo].[UserInRole] ([UserID], [RoleID])
	VALUES (
		(SELECT [UserID] FROM [UserInfo].[User] WHERE [Login] = @Login),
		(SELECT [RoleID] FROM [UserInfo].[Role] WHERE [RoleName] = @RoleName)
	)
	SELECT 1;
	RETURN
END
ELSE
BEGIN
	SELECT -1;
	RETURN
END