CREATE PROCEDURE [UserInfo].[DeleteRole] 
@RoleName nvarchar(10)
AS
DELETE FROM [UserInfo].[Role] WHERE [RoleName] = @RoleName;