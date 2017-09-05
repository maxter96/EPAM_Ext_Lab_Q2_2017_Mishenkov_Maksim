SET IDENTITY_INSERT [UserInfo].[User] ON;
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (1, 'Administrator', '35106240-d2f0-db35-716e-127eb80a0299', N'Максим', N'Мишенков');
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (2, 'FirstUser', '2b07acc5-1aa6-f20c-df18-098d9fbc4cca', N'Иван', N'Петров');
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (3, 'SecondUser', '3bb5e3ce-c9a4-7b43-1699-2f0ebcd0ac5b', N'Сергей', N'Васильков');
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (4, 'ThirdUser', '7426f1e3-722e-0277-b18f-4fd43b78f596', N'Петр', N'Иванов');
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (5, 'FourthUser', '38f8ce4f-65c2-ca68-81b9-866a211f5b23', N'Сергей', N'Васильков');
INSERT [UserInfo].[User] ([UserID], [Login], [Password], [FirstName], [LastName]) 
	VALUES (6, 'FifthUser', '15ee5daa-051b-00d7-7de1-f6ccf4dd1802', N'Илья', N'Антонов');
SET IDENTITY_INSERT [UserInfo].[User] OFF;




