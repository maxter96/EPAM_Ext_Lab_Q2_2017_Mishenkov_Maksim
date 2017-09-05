CREATE PROCEDURE [UserInfo].[DeleteUser] 
@UserID int
AS
DELETE FROM [UserInfo].[User]
WHERE UserID = @UserID;