SET IDENTITY_INSERT [TestInfo].[Question] ON
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (1, 1, N'Сколько будет 2x2', 1);
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (2, 1, N'Сколько будет 5x5', 2);
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (3, 1, N'У Васи было 5 классов. Вася добавил еще 24. Сколько классов стало у Васи?',	1);
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (4, 2, N'Выберите слова с неправильным ударением', 2);
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (5, 2, N'Выберите правильно написанные слова', 2);
INSERT [TestInfo].[Question] ([QuestionID], [ThemeID], [QuestionText], [QuestionType])
VALUES (6, 2, N'Выберите прилагательное', 1);
SET IDENTITY_INSERT [TestInfo].[Question] OFF