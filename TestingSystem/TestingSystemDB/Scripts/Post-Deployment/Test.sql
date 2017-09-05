SET IDENTITY_INSERT [TestInfo].[Test] ON
INSERT [TestInfo].[Test] ([TestID], [TestName], [TimeLimit], [AttemptsCount]) VALUES(1, N'Общий тест', 10, 3);
INSERT [TestInfo].[Test] ([TestID], [TestName], [TimeLimit], [AttemptsCount]) VALUES(2, N'Тест по русскому языку', 10, 2);
SET IDENTITY_INSERT [TestInfo].[Test] OFF