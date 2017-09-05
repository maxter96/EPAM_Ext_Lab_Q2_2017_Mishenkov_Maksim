CREATE PROCEDURE [UserInfo].[GetRolesForUser] 
@Login nvarchar(20)
AS
SELECT r.[RoleName] FROM [UserInfo].[User] AS u
JOIN [UserInfo].[UserInRole] AS ur
ON u.[UserID] = ur.[UserID]
JOIN [UserInfo].[Role] AS r
ON ur.[RoleID] = r.[RoleID]
WHERE u.Login = @Login;