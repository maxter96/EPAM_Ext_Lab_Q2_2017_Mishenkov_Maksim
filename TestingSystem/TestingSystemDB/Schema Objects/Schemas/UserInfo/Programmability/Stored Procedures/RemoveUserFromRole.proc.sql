CREATE PROCEDURE [UserInfo].[RemoveUserFromRole] 
@Login nvarchar(50),
@RoleName nvarchar(10)
AS
DELETE FROM [UserInfo].[UserInRole]
WHERE [UserID] = (SELECT [UserID] FROM [UserInfo].[User] WHERE [Login] = @Login)
AND [RoleID] = (SELECT [RoleID] FROM [UserInfo].[Role] WHERE [RoleName] = @RoleName)