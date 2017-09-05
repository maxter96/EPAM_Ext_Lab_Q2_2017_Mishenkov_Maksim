SET IDENTITY_INSERT [TestInfo].[TestSession] ON
INSERT [TestInfo].[TestSession] ([TestSessionID], [UserID], [TestID], [BeginTime], [EndTime], [Time], [QuestionsCount], [RightAnswers])
VALUES(1, 3, 1, '2017-09-06 01:49:44.067', '2017-09-06 01:59:44.067', '2017-09-06 01:54:13.073', 6, 5);
INSERT [TestInfo].[TestSession] ([TestSessionID], [UserID], [TestID], [BeginTime], [EndTime], [Time], [QuestionsCount], [RightAnswers])
VALUES(2, 3, 1, '2017-09-06 01:55:20.003', '2017-09-06 02:05:20.003', '2017-09-06 01:58:39.707', 6, 6);
INSERT [TestInfo].[TestSession] ([TestSessionID], [UserID], [TestID], [BeginTime], [EndTime], [Time], [QuestionsCount], [RightAnswers])
VALUES(3, 4, 2, '2017-09-06 01:51:08.797', '2017-09-06 02:01:08.797', '2017-09-06 01:53:32.660', 3, 1);
INSERT [TestInfo].[TestSession] ([TestSessionID], [UserID], [TestID], [BeginTime], [EndTime], [Time], [QuestionsCount], [RightAnswers])
VALUES(4, 4, 2, '2017-09-06 01:54:37.763', '2017-09-06 02:04:37.763', '2017-09-06 01:57:04.287', 3, 2);
SET IDENTITY_INSERT [TestInfo].[TestSession] OFF