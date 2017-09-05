/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE FROM [TestInfo].[ThemeStatistic];
DELETE FROM [TestInfo].[TestSession];
DELETE FROM [TestInfo].[UserAnswer];
DELETE FROM [TestInfo].[Task];
DELETE FROM [TestInfo].[Test];
DELETE FROM [TestInfo].[Answer];
DELETE FROM [TestInfo].[Question];
DELETE FROM [TestInfo].[Theme];
DELETE FROM [UserInfo].[UserInRole];
DELETE FROM [UserInfo].[Role];
DELETE FROM [UserInfo].[User];

:r .\User.sql
:r .\Role.sql
:r .\UserInRole.sql
:r .\Test.sql
:r .\Theme.sql
:r .\Question.sql
:r .\Answer.sql
:r .\Task.sql
:r .\TestSession.sql
:r .\UserAnswer.sql
:r .\ThemeStatistic.sql
