using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
	public class UserController : Controller
	{
		[Authorize(Roles = "User")]
		public ActionResult RunTest(int testId)
		{
			var userRepo = new UsersRepository();
			var user = userRepo.GetUserInfo(User.Identity.Name);

			if (user == null)
			{
				return HttpNotFound();
			}

			var testRepo = new TestRepository();
			var testSessionId = testRepo.CreateTestSession(new DataAccessLayer.Models.UserTestModel
			{
				TestID = testId,
				UserID = user.UserID
			});

			if (testSessionId == TestRepository.DefaultErrorCode)
			{
				return RedirectToAction("MyTests");
			}

			var test = testRepo.GetTestInfo(testId);
			var questions = testRepo.GetTestQuestions(testId);
			var userAnswers = testRepo.GetUserAnswers(testSessionId);

			var viewQuestions = new List<QuestionWithUserAnswerViewModel>();

			foreach (var question in questions)
			{
				var questionAnswers = testRepo
					.GetQuestionAnswers(question.QuestionID)
					.Select(x => new UserAnswerViewModel
					{
						AnswerID = x.AnswerID,
						AnswerText = x.AnswerText,
						IsChecked = userAnswers.Contains(x.AnswerID)
					})
					.ToList();
				viewQuestions.Add(new QuestionWithUserAnswerViewModel
				{
					QuestionText = question.QuestionText,
					Answers = questionAnswers
				});
			}

			var viewTest = new RunningTestViewModel
			{
				UserID = user.UserID,
				TestSessionID = testSessionId,
				TestName = test.TestName,
				Questions = viewQuestions
			};

			return View(viewTest);
		}

		[HttpPost]
		[Authorize(Roles = "User")]
		public int CheckTestSession(int testSessionId)
		{
			var testRepo = new TestRepository();
			var testSessionInfo = testRepo.GetTestSessionInfo(testSessionId);
			return (testSessionInfo.EndTime - testSessionInfo.BeginTime).Minutes;
		}

		[HttpPost]
		[Authorize(Roles = "User")]
		public void CreateUserAnswer(CreatingUserAnswerViewModel userAnswer)
		{
			var testRep = new TestRepository();
			testRep.CreateUserAnswer(new DataAccessLayer.Models.CreatingUserAnswerModel
			{
				AnswerID = userAnswer.AnswerID,
				TestSessionID = userAnswer.TestSessionID
			});
		}

		[HttpPost]
		[Authorize(Roles = "User")]
		public void DeleteUserAnswer(CreatingUserAnswerViewModel userAnswer)
		{
			var testRep = new TestRepository();
			testRep.DeleteUserAnswer(new DataAccessLayer.Models.CreatingUserAnswerModel
			{
				AnswerID = userAnswer.AnswerID,
				TestSessionID = userAnswer.TestSessionID
			});
		}

		[HttpPost]
		public ActionResult EndTestSession(UserSessionViewModel user)
		{
			var testRepo = new TestRepository();
			var result = testRepo.EndTestSession(user.TestSessionID);

			if (result == TestRepository.DefaultErrorCode)
			{
				return Json("MyTests", JsonRequestBehavior.AllowGet);
			}

			return Json("TestSessionResult?testSessionId=" 
				+ user.TestSessionID + "&userId=" + user.UserID
				, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public int GetRemainingTime(int testSessionId)
		{
			var rep = new TestRepository();
			return rep.GetRemainingTime(testSessionId);
		}

		private List<UserRatingViewModel> GetTestsRating()
		{
			var viewUsers = new List<UserRatingViewModel>();
			var userRepo = new UsersRepository();
			var testRepo = new TestRepository();
			var users = userRepo.GetAllUsers();
			var tests = testRepo.GetAllTests();
			var questionsCount = tests
				.Sum(x => testRepo.GetTestQuestions(x.TestID).Count());

			foreach (var user in users)
			{
				var userId = user.UserID;
				var userName = string.Format("{0} {1} ({2})",
					user.FirstName, user.LastName, user.Login);

				var sessions = tests
					.Select(x => testRepo.GetUserTestSessions(new DataAccessLayer.Models.UserTestModel
					{
						UserID = userId,
						TestID = x.TestID
					})
					.LastOrDefault(y => y.Time.HasValue))
					.Where(x => x != null);

				var score = sessions.Sum(x => x.RightAnswers);

				viewUsers.Add(new UserRatingViewModel
				{
					UserID = userId,
					UserName = string
						.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Login),
					Score = score,
					QuestionsCount = questionsCount
				});
			}

			return viewUsers.OrderByDescending(x => x.Score)
				.ThenBy(x => x.UserName)
				.ToList();
		}

		private List<UserRatingViewModel> GetThemeRating(int themeId)
		{
			var viewUsers = new List<UserRatingViewModel>();
			var userRepo = new UsersRepository();
			var testRepo = new TestRepository();
			var users = userRepo.GetAllUsers();
			var tests = testRepo.GetAllTests();
			var questionsCount = tests
				.SelectMany(x => testRepo.GetTestQuestions(x.TestID))
				.Where(x => x.ThemeID == themeId)
				.Count();

			foreach (var user in users)
			{
				var userId = user.UserID;
				var userName = string.Format("{0} {1} ({2})",
					user.FirstName, user.LastName, user.Login);

				var sessions = tests
					.Select(x => testRepo.GetUserTestSessions(new DataAccessLayer.Models.UserTestModel
					{
						UserID = userId,
						TestID = x.TestID
					})
					.LastOrDefault(y => y.Time.HasValue))
					.Where(x => x != null);

				var score = sessions
					.SelectMany(x => testRepo.GetThemeStatistic(x.TestSessionID))
					.Where(x => x.ThemeID == themeId)
					.Sum(x => x.RightAnswers);

				viewUsers.Add(new UserRatingViewModel
				{
					UserID = userId,
					UserName = string
						.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Login),
					Score = score,
					QuestionsCount = questionsCount
				});
			}

			return viewUsers.OrderByDescending(x => x.Score)
				.ThenBy(x => x.UserName)
				.ToList();
		}

		[Authorize]
		public ActionResult Rating(int? themeId)
		{
			var testRepo = new TestRepository();
			var themes = testRepo.GetAllThemes();
			ViewBag.Themes = themes;

			if (!themeId.HasValue || themeId == 0)
			{
				var viewUsers = GetTestsRating();
				var viewRating = new RatingViewModel
				{
					RatingName = Resources.Resource.MES_CommonRating,
					Users = viewUsers
				};
				return View(viewRating);
			}

			var theme = testRepo.GetThemeInfo(themeId.Value);

			if (theme == null)
			{
				return HttpNotFound();
			}
			else
			{
				var viewUsers = GetThemeRating(theme.ThemeID);
				var viewRating = new RatingViewModel
				{
					RatingName = theme.ThemeName,
					Users = viewUsers
				};
				return View(viewRating);
			}
		}

		[Authorize]
		public ActionResult MyTests(int? userId)
		{
			var userRepo = new UsersRepository();
			DataAccessLayer.Models.UserModel user = null;

			if (!userId.HasValue)
			{
				if (User.IsInRole("Admin"))
				{
					return HttpNotFound();
				}
				else
				{
					user = userRepo.GetUserInfo(User.Identity.Name);
				}
			}
			else
			{
				user = userRepo
					.GetAllUsers()
					.Where(x => x.UserID == userId)
					.FirstOrDefault();
				var checkingUser = userRepo.GetUserInfo(User.Identity.Name);

				if (!User.IsInRole("Admin") && user.UserID != checkingUser.UserID)
				{
					return HttpNotFound();
				}
			}

			if (user == null)
			{
				return HttpNotFound();
			}

			var testRepo = new TestRepository();
			var tests = testRepo.GetAllTests();
			var viewTests = new List<UserTestViewModel>();

			foreach (var test in tests)
			{
				var questionsCount = testRepo.GetTestQuestions(test.TestID).Count;

				var sessions = testRepo.GetUserTestSessions(new DataAccessLayer.Models.UserTestModel
				{
					TestID = test.TestID,
					UserID = user.UserID
				});

				var lastSession = sessions.LastOrDefault(x => x.Time != null);

				var testView = new UserTestViewModel
				{
					UserID = user.UserID,
					TestID = test.TestID,
					TestName = test.TestName,
					RemainingAttempts = test.AttemptsCount - sessions.Count,
					LastScore = lastSession == null ? 0 : lastSession.RightAnswers,
					QuestionsCount = questionsCount
				};

				viewTests.Add(testView);
			}

			var userResults = new UserResultsViewModel
			{
				UserName = string.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Login),
				Tests = viewTests
			};

			return View(userResults);
		}

		[Authorize]
		public ActionResult TestResults(int testId, int userId)
		{
			var userRepo = new UsersRepository();
			var testRepo = new TestRepository();
			var user = userRepo
				.GetAllUsers()
				.Where(x => x.UserID == userId)
				.FirstOrDefault();
			var checkingUser = userRepo.GetUserInfo(User.Identity.Name);

			if (user == null || !User.IsInRole("Admin") && user.UserID != checkingUser.UserID)
			{
				return HttpNotFound();
			}

			var test = testRepo.GetTestInfo(testId);
			var questionsCount = testRepo.GetTestQuestions(test.TestID).Count;

			if (test == null)
			{
				return RedirectToAction("MyTests");
			}

			var sessions = testRepo.GetUserTestSessions(new DataAccessLayer.Models.UserTestModel
			{
				TestID = test.TestID,
				UserID = user.UserID
			});

			var viewsSessions = new List<UserAttemptViewModel>();
			var index = 1;

			foreach (var session in sessions)
			{
				viewsSessions.Add(new UserAttemptViewModel
				{
					UserID = user.UserID,
					AttemptNumber = index,
					Score = session.Time.HasValue ? string
						.Format("{0}/{1}", session.RightAnswers, session.QuestionsCount) : "n/a",
					Time = session.Time == null ? -1 : (session.Time.Value - session.BeginTime).Minutes,
					TestID = test.TestID,
					TestSessionID = session.TestSessionID
				});

				index++;
			}

			var viewTest = new UserTestWithSessionsViewModel
			{
				UserID = userId,
				TestID = test.TestID,
				TestName = test.TestName,
				AttemptsCount = test.AttemptsCount,
				Sessions = viewsSessions,
				AttemptsLeft = test.AttemptsCount - viewsSessions.Count
			};

			return View("MyTestsWithAttempts", viewTest);
		}

		[Authorize]
		public ActionResult TestSessionResult(int testSessionId, int userId)
		{
			var testRepo = new TestRepository();
			var userRepo = new UsersRepository();
			var user = userRepo.GetUserInfo(User.Identity.Name);

			if (!User.IsInRole("Admin"))
			{
				var userSessions = testRepo
					.GetAllTests()
					.SelectMany(x => testRepo.GetUserTestSessions(new DataAccessLayer.Models.UserTestModel
					{
						UserID = user.UserID,
						TestID = x.TestID
					}));

				if (!userSessions.Select(x => x.TestSessionID).Contains(testSessionId))
				{
					return HttpNotFound();
				}
			}

			var testSession = testRepo.GetTestSessionInfo(testSessionId);

			if (testSession == null)
			{
				return RedirectToAction("MyTests", new { userId = userId });
			}

			var test = testRepo.GetTestInfo(testSession.TestID);
			var questions = testRepo.GetTestQuestions(testSession.TestID);
			var userAnswers = testRepo.GetUserAnswers(testSessionId);
			var viewQuestions = new List<QuestonWithResultViewModel>();

			foreach (var question in questions)
			{
				var questionAnswers = testRepo
					.GetQuestionAnswers(question.QuestionID)
					.Select(x => new UserAnswerViewModel
					{
						AnswerID = x.AnswerID,
						AnswerText = x.AnswerText,
						IsChecked = userAnswers.Contains(x.AnswerID)
					})
					.ToList();

				var points = testRepo.GetQuestionPoints(new DataAccessLayer.Models.QuestionSessionModel
				{
					QuestionID = question.QuestionID,
					TestSessionID = testSession.TestSessionID
				});

				viewQuestions.Add(new QuestonWithResultViewModel
				{
					QuestionText = question.QuestionText,
					Answers = questionAnswers,
					Points = points
				});
			}

			var viewTest = new UserTestSessionResultViewModel
			{
				UserID = userId,
				TestID = test.TestID,
				TestName = test.TestName,
				Questions = viewQuestions,
				Score = string.Format("{0}/{1}", viewQuestions.Sum(x => x.Points), viewQuestions.Count),
			};

			return View(viewTest);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult DeleteUser(int userId)
		{
			var userRepo = new UsersRepository();
			userRepo.DeleteUser(userId);
			return Json(string.Empty, JsonRequestBehavior.AllowGet);
		}
	}
}