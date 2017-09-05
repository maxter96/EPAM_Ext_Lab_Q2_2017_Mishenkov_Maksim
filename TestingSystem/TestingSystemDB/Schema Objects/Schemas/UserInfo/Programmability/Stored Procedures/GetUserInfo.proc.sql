CREATE PROCEDURE [UserInfo].[GetUserInfo] 
@Login nvarchar(20)
AS
SELECT [UserID]
, [Login]
, [FirstName]
, [LastName]
, [Password] 
FROM [UserInfo].[User]
WHERE [Login] = @Login;