CREATE PROCEDURE [UserInfo].[CreateUser] 
@Login nvarchar(20),
@Password uniqueidentifier,
@FirstName nvarchar(20),
@LastName nvarchar(20)
AS
IF NOT EXISTS (SELECT [UserID] FROM [UserInfo].[User] WHERE [Login] = @Login)
BEGIN
	INSERT INTO [UserInfo].[User] (Login, Password, FirstName, LastName)
	VALUES (@Login, @Password, @FirstName, @LastName);
	SELECT CONVERT(int, SCOPE_IDENTITY());
	RETURN;
END
ELSE
BEGIN
	SELECT -1;
	RETURN;
END