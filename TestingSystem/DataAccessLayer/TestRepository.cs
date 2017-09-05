using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DataAccessLayer.Models;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DataAccessLayer
{
	public class TestRepository
	{
		public const int DefaultErrorCode = -1;

		public const int MaxThemeNameLength = 50;

		private DbProviderFactory factory;

		private string connectionString;

		public TestRepository()
		{
			var connectionStringItem = ConfigurationManager.ConnectionStrings["TestingSystemDBConnection"];
			connectionString = connectionStringItem.ConnectionString;
			var providerName = connectionStringItem.ProviderName;
			factory = DbProviderFactories.GetFactory(providerName);
		}

		public List<ThemeModel> GetAllThemes()
		{
			var themes = new List<ThemeModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT [ThemeID], [ThemeName], " +
					"(SELECT COUNT([QuestionID]) " +
					"FROM [TestInfo].[Question] AS q " +
					"WHERE q.[ThemeID] = t.[ThemeID]) " +
					"AS [QuestionsCount] " +
					"FROM [TestInfo].[Theme] AS t " +
					"ORDER BY [ThemeName];";
				command.CommandType = CommandType.Text;
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var theme = new ThemeModel
						{
							ThemeID = (int)reader["ThemeID"],
							ThemeName = (string)reader["ThemeName"],
							QuestionsCount = (int)reader["QuestionsCount"]
						};
						themes.Add(theme);
					}
				}
			}

			return themes;
		}

		public int CreateTheme(CreatingThemeModel theme)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateTheme]";
				command.CommandType = CommandType.StoredProcedure;
				var themeNameParam = command.CreateParameter();
				themeNameParam.ParameterName = "@ThemeName";
				themeNameParam.DbType = DbType.StringFixedLength;
				themeNameParam.Value = theme.ThemeName;
				command.Parameters.Add(themeNameParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int DeleteTheme(int themeId)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[DeleteTheme]";
				command.CommandType = CommandType.StoredProcedure;
				var themeIdParam = command.CreateParameter();
				themeIdParam.ParameterName = "@ThemeID";
				themeIdParam.DbType = DbType.Int32;
				themeIdParam.Value = themeId;
				command.Parameters.Add(themeIdParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public ThemeModel GetThemeInfo(int themeId)
		{
			return GetAllThemes()
				.Where(x => x.ThemeID == themeId)
				.FirstOrDefault();
		}

		public List<QuestionModel> GetThemeQuestions(int themeId)
		{
			var questions = new List<QuestionModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetThemeQuestions]";
				command.CommandType = CommandType.StoredProcedure;

				var themeIdParam = command.CreateParameter();
				themeIdParam.ParameterName = "@ThemeID";
				themeIdParam.DbType = DbType.Int32;
				themeIdParam.Value = themeId;

				command.Parameters.Add(themeIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var question = new QuestionModel
						{
							QuestionID = (int)reader["QuestionID"],
							ThemeID = (int)reader["ThemeID"],
							QuestionText = (string)reader["QuestionText"],
							QuestionType = (int)reader["QuestionType"]
						};
						questions.Add(question);
					}
				}
			}

			return questions;
		}

		public int DeleteQuestion(int questionId)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[DeleteQuestion]";
				command.CommandType = CommandType.StoredProcedure;
				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = questionId;
				command.Parameters.Add(questionIdParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int CreateQuestion(CreatingQuestionModel question)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateQuestion]";
				command.CommandType = CommandType.StoredProcedure;

				var questionTextParam = command.CreateParameter();
				questionTextParam.ParameterName = "@QuestionText";
				questionTextParam.DbType = DbType.StringFixedLength;
				questionTextParam.Value = question.QuestionText;

				var questionTypeParam = command.CreateParameter();
				questionTypeParam.ParameterName = "@QuestionType";
				questionTypeParam.DbType = DbType.Int32;
				questionTypeParam.Value = question.QuestionType;

				var themeIdParam = command.CreateParameter();
				themeIdParam.ParameterName = "@ThemeID";
				themeIdParam.DbType = DbType.Int32;
				themeIdParam.Value = question.ThemeID;

				command.Parameters.AddRange(new[] { questionTextParam, questionTypeParam, themeIdParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int CreateAnswer(CreatingAnswerModel answer)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateAnswer]";
				command.CommandType = CommandType.StoredProcedure;

				var answerTextParam = command.CreateParameter();
				answerTextParam.ParameterName = "@AnswerText";
				answerTextParam.DbType = DbType.StringFixedLength;
				answerTextParam.Value = answer.AnswerText;

				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = answer.QuestionID;

				var isCorrectParam = command.CreateParameter();
				isCorrectParam.ParameterName = "@IsCorrect";
				isCorrectParam.DbType = DbType.Boolean;
				isCorrectParam.Value = answer.IsCorrect;

				command.Parameters.AddRange(new[] { answerTextParam, questionIdParam, isCorrectParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public List<AnswerModel> GetQuestionAnswers(int questionId)
		{
			var answers = new List<AnswerModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetQuestionAnswers]";
				command.CommandType = CommandType.StoredProcedure;

				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = questionId;

				command.Parameters.Add(questionIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var answer = new AnswerModel
						{
							QuestionID = (int)reader["QuestionID"],
							AnswerText = (string)reader["AnswerText"],
							AnswerID = (int)reader["AnswerID"],
							IsCorrect = (bool)reader["IsCorrect"]
						};
						answers.Add(answer);
					}
				}
			}

			return answers;
		}

		public QuestionModel GetQuestionInfo(int questionId)
		{
			var questions = new List<QuestionModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetQuestionInfo]";
				command.CommandType = CommandType.StoredProcedure;

				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = questionId;

				command.Parameters.Add(questionIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var theme = new QuestionModel
						{
							ThemeID = (int)reader["ThemeID"],
							QuestionID = (int)reader["QuestionID"],
							QuestionText = (string)reader["QuestionText"],
							QuestionType = (int)reader["QuestionType"]
						};
						questions.Add(theme);
					}
				}
			}

			return questions.FirstOrDefault();
		}

		public List<TestModel> GetAllTests()
		{
			var tests = new List<TestModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT [TestID], [TestName], [TimeLimit], [AttemptsCount] " +
					"FROM [TestInfo].[Test] ORDER BY [TestName]";
				command.CommandType = CommandType.Text;
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var test = new TestModel
						{
							TestID = (int)reader["TestID"],
							TestName = (string)reader["TestName"],
							TimeLimit = (int)reader["TimeLimit"],
							AttemptsCount = (int)reader["AttemptsCount"]
						};
						tests.Add(test);
					}
				}
			}

			return tests;
		}

		public TestModel GetTestInfo(int testId)
		{
			return GetAllTests()
				.Where(x => x.TestID == testId)
				.FirstOrDefault();
		}

		public List<QuestionModel> GetTestQuestions(int testId)
		{
			var questions = new List<QuestionModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetTestQuestions]";
				command.CommandType = CommandType.StoredProcedure;

				var testIdParam = command.CreateParameter();
				testIdParam.ParameterName = "@TestID";
				testIdParam.DbType = DbType.Int32;
				testIdParam.Value = testId;

				command.Parameters.Add(testIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var question = new QuestionModel
						{
							QuestionID = (int)reader["QuestionID"],
							QuestionText = (string)reader["QuestionText"],
							QuestionType = (int)reader["QuestionType"],
							ThemeID = (int)reader["ThemeID"]
						};
						questions.Add(question);
					}
				}
			}

			return questions;
		}

		public void DeleteTest(int testId)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[DeleteTest]";
				command.CommandType = CommandType.StoredProcedure;
				var testIdParam = command.CreateParameter();
				testIdParam.ParameterName = "@TestID";
				testIdParam.DbType = DbType.Int32;
				testIdParam.Value = testId;
				command.Parameters.Add(testIdParam);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public int CreateTest(CreatingTestModel test)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateTest]";
				command.CommandType = CommandType.StoredProcedure;

				var testNameParam = command.CreateParameter();
				testNameParam.ParameterName = "@TestName";
				testNameParam.DbType = DbType.StringFixedLength;
				testNameParam.Value = test.TestName;

				var timeLimitParam = command.CreateParameter();
				timeLimitParam.ParameterName = "@TimeLimit";
				timeLimitParam.DbType = DbType.Int32;
				timeLimitParam.Value = test.TimeLimit;

				var attemptsParam = command.CreateParameter();
				attemptsParam.ParameterName = "@AttemptsCount";
				attemptsParam.DbType = DbType.Int32;
				attemptsParam.Value = test.AttemptsCount;

				command.Parameters.AddRange(new[] { testNameParam, timeLimitParam, attemptsParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int CreateTask(CreatingTaskModel task)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateTask]";
				command.CommandType = CommandType.StoredProcedure;

				var testIdParam = command.CreateParameter();
				testIdParam.ParameterName = "@TestID";
				testIdParam.DbType = DbType.Int32;
				testIdParam.Value = task.TestID;

				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = task.QuestionID;

				command.Parameters.AddRange(new[] { testIdParam, questionIdParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int CreateTestSession(UserTestModel userAndTest)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateTestSession]";
				command.CommandType = CommandType.StoredProcedure;

				var testIdParam = command.CreateParameter();
				testIdParam.ParameterName = "@TestID";
				testIdParam.DbType = DbType.Int32;
				testIdParam.Value = userAndTest.TestID;

				var userIdParam = command.CreateParameter();
				userIdParam.ParameterName = "@UserID";
				userIdParam.DbType = DbType.Int32;
				userIdParam.Value = userAndTest.UserID;

				command.Parameters.AddRange(new[] { testIdParam, userIdParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int EndTestSession(int testSessionId)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[EndTestSession]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = testSessionId;


				command.Parameters.Add(testSesIdParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public List<int> GetUserAnswers(int testSessionId)
		{
			var answers = new List<int>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetUserAnswers]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = testSessionId;

				command.Parameters.Add(testSesIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						answers.Add((int)reader["AnswerID"]);
					}
				}
			}

			return answers;
		}

		public TestSessionModel GetTestSessionInfo(int testSessionId)
		{
			var sessions = new List<TestSessionModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetTestSessionInfo]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = testSessionId;

				command.Parameters.Add(testSesIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var session = new TestSessionModel
						{
							TestSessionID = (int)reader["TestSessionID"],
							TestID = (int)reader["TestID"],
							UserID = (int)reader["UserID"],
							BeginTime = (DateTime)reader["BeginTime"],
							EndTime = (DateTime)reader["EndTime"],
							QuestionsCount = (int)reader["QuestionsCount"],
							RightAnswers = (int)reader["RightAnswers"],
						};

						if (!reader.IsDBNull(reader.GetOrdinal("Time")))
						{
							session.Time = (DateTime)reader["Time"];
						}

						sessions.Add(session);
					}
				}
			}

			return sessions.FirstOrDefault();
		}

		public int CreateUserAnswer(CreatingUserAnswerModel userAnswer)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[CreateUserAnswer]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = userAnswer.TestSessionID;

				var answerIdParam = command.CreateParameter();
				answerIdParam.ParameterName = "@AnswerID";
				answerIdParam.DbType = DbType.Int32;
				answerIdParam.Value = userAnswer.AnswerID;

				command.Parameters.AddRange(new[] { testSesIdParam, answerIdParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public void DeleteUserAnswer(CreatingUserAnswerModel userAnswer)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[DeleteUserAnswer]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = userAnswer.TestSessionID;

				var answerIdParam = command.CreateParameter();
				answerIdParam.ParameterName = "@AnswerID";
				answerIdParam.DbType = DbType.Int32;
				answerIdParam.Value = userAnswer.AnswerID;

				command.Parameters.AddRange(new[] { testSesIdParam, answerIdParam });
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public int GetRemainingTime(int testSessionId)
		{
			var result = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "SELECT [TestInfo].[GetRemainingTime] (@TestSessionID)";
				command.CommandType = CommandType.Text;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = testSessionId;

				command.Parameters.Add(testSesIdParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public List<TestSessionModel> GetUserTestSessions(UserTestModel userAndTest)
		{
			var sessions = new List<TestSessionModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetUserTestSessions]";
				command.CommandType = CommandType.StoredProcedure;

				var testIdParam = command.CreateParameter();
				testIdParam.ParameterName = "@TestID";
				testIdParam.DbType = DbType.Int32;
				testIdParam.Value = userAndTest.TestID;

				var userIdParam = command.CreateParameter();
				userIdParam.ParameterName = "@UserID";
				userIdParam.DbType = DbType.Int32;
				userIdParam.Value = userAndTest.UserID;

				command.Parameters.AddRange(new[] { testIdParam, userIdParam });
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var session = new TestSessionModel
						{
							TestSessionID = (int)reader["TestSessionID"],
							TestID = (int)reader["TestID"],
							UserID = (int)reader["UserID"],
							BeginTime = (DateTime)reader["BeginTime"],
							EndTime = (DateTime)reader["EndTime"],
							QuestionsCount = (int)reader["QuestionsCount"],
							RightAnswers = (int)reader["RightAnswers"],
						};

						if (!reader.IsDBNull(reader.GetOrdinal("Time")))
						{
							session.Time = (DateTime)reader["Time"];
						}

						sessions.Add(session);
					}
				}
			}

			return sessions;
		}

		public List<ThemeStatisticModel> GetThemeStatistic(int testSessionId)
		{
			var statistics = new List<ThemeStatisticModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[TestInfo].[GetThemeStatistic]";
				command.CommandType = CommandType.StoredProcedure;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = testSessionId;


				command.Parameters.Add(testSesIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var statistic = new ThemeStatisticModel
						{
							ThemeStatisticID = (int)reader["ThemeStatisticID"],
							TestSessionID = (int)reader["TestSessionID"],
							QuestionsCount = (int)reader["QuestionsCount"],
							RightAnswers = (int)reader["RightAnswers"],
							ThemeID = (int)reader["ThemeID"]
						};

						statistics.Add(statistic);
					}
				}
			}

			return statistics;
		}

		public int GetQuestionPoints(QuestionSessionModel sesQuestion)
		{
			var result = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = 
					"SELECT [TestInfo].[GetQuestionPoints] " +
					"(@TestSessionID, @QuestionID)";
				command.CommandType = CommandType.Text;

				var testSesIdParam = command.CreateParameter();
				testSesIdParam.ParameterName = "@TestSessionID";
				testSesIdParam.DbType = DbType.Int32;
				testSesIdParam.Value = sesQuestion.TestSessionID;

				var questionIdParam = command.CreateParameter();
				questionIdParam.ParameterName = "@QuestionID";
				questionIdParam.DbType = DbType.Int32;
				questionIdParam.Value = sesQuestion.QuestionID;

				command.Parameters.AddRange(new[] { testSesIdParam, questionIdParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}
	}
}
