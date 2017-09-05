CREATE PROCEDURE [UserInfo].[GetUsersInRole] 
@RoleName nvarchar(10)
AS
SELECT u.[Login] FROM [UserInfo].[Role] AS r
JOIN [UserInfo].[UserInRole] AS ur
ON r.[RoleID] = ur.[RoleID]
JOIN [UserInfo].[User] AS u
ON ur.[UserID] = u.[UserID]
WHERE r.[RoleName] = @RoleName;