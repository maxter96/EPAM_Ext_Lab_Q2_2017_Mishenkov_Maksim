using AutoMapper;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
	[Authorize(Roles="Admin")]
	public class ManagementController : Controller
	{
		public ActionResult Users()
		{
			return View();
		}

		public ActionResult Themes()
		{
			var testRepo = new TestRepository();
			var themes = testRepo.GetAllThemes();
			var viewThemes = new List<ThemeViewModel>();
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.ThemeModel,
				ThemeViewModel>());

			foreach (var theme in themes)
			{
				var viewTheme = Mapper.Map<DataAccessLayer.Models.ThemeModel,
					ThemeViewModel>(theme);
				viewThemes.Add(viewTheme);
			}

			return View(viewThemes);
		}

		public ActionResult CreateTheme()
		{
			var creatingTheme = new CreatingThemeViewModel();
			return PartialView("_CreatingThemePartial", creatingTheme);
		}

		[HttpPost]
		public ActionResult CreateTheme(CreatingThemeViewModel theme)
		{
			var testRepo = new TestRepository();
			var creatingTheme = new DataAccessLayer.Models.CreatingThemeModel
			{
				ThemeName = theme.ThemeName
			};
			var res = testRepo.CreateTheme(creatingTheme);

			if (res == TestRepository.DefaultErrorCode)
			{
				return Json(Resources.Resource.ERROR_ThemeExists);
			}

			return Json(string.Empty);
		}

		public ActionResult DeleteTheme(int themeId)
		{
			var testRepo = new TestRepository();
			var res = testRepo.DeleteTheme(themeId);

			if (res == TestRepository.DefaultErrorCode)
			{
				return Json(Resources.Resource.ERROR_ThemeContainsInTest,
					JsonRequestBehavior.AllowGet);
			}

			return Json(string.Empty, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ThemeDetails(int themeId)
		{
			var testRepo = new TestRepository();
			var theme = testRepo.GetThemeInfo(themeId);

			if (theme == null)
			{
				RedirectToAction("Themes");
			}

			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.QuestionModel,
				QuestionViewModel>());
			var questions = testRepo.GetThemeQuestions(themeId)
				.Select(x => Mapper.Map<DataAccessLayer.Models.QuestionModel, QuestionViewModel>(x))
				.ToList();

			var themeDetails = new ThemeDetailsViewModel
			{
				ThemeID = theme.ThemeID,
				ThemeName = theme.ThemeName,
				QuestionsCount = theme.QuestionsCount,
				Questions = questions
			};

			return View(themeDetails);
		}

		public ActionResult DeleteQuestion(int questionId)
		{
			var testRepo = new TestRepository();
			var res = testRepo.DeleteQuestion(questionId);

			if (res == TestRepository.DefaultErrorCode)
			{
				return Json(Resources.Resource.ERROR_QuestionContainsInTest,
					JsonRequestBehavior.AllowGet);
			}

			return Json(string.Empty, JsonRequestBehavior.AllowGet);
		}

		public ActionResult CreateQuestion(int themeId)
		{
			var question = new CreatingQuestionViewModel()
			{
				Answers = new List<CreatingAnswerViewModel>()
			};

			for (var i = 0; i < 4; i++)
			{
				question.Answers.Add(new CreatingAnswerViewModel());
			}

			return PartialView("_CreatingQuestionPartial", question);
		}

		[HttpPost]
		public ActionResult CreateQuestion(CreatingQuestionViewModel question)
		{
			var rightAnswers = question.Answers.Where(x => x.IsCorrect).Count();

			if (rightAnswers < 1)
			{
				return Json(Resources.Resource.ERROR_QuestionMustContainRightAnswers);
			}

			var creatingQuestion = new DataAccessLayer.Models.CreatingQuestionModel
			{
				QuestionText = question.QuestionText,
				QuestionType = rightAnswers,
				ThemeID = question.ThemeID
			};

			var rep = new TestRepository();
			var questionId = rep.CreateQuestion(creatingQuestion);

			if (questionId == TestRepository.DefaultErrorCode)
			{
				return Json(Resources.Resource.ERROR_QuestionExists);
			}

			foreach (var answer in question.Answers)
			{
				var creatingAnswer = new DataAccessLayer.Models.CreatingAnswerModel
				{
					QuestionID = questionId,
					AnswerText = answer.AnswerText,
					IsCorrect = answer.IsCorrect
				};
				rep.CreateAnswer(creatingAnswer);
			}

			return Json(string.Empty);
		}

		public ActionResult QuestionDetails(int questionId)
		{
			var testRepo = new TestRepository();
			var question = testRepo.GetQuestionInfo(questionId);
			var answers = testRepo.GetQuestionAnswers(questionId);

			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.QuestionModel,
				QuestionWithAnswersViewModel>());
			var viewQuestion = Mapper.Map<DataAccessLayer.Models.QuestionModel,
				QuestionWithAnswersViewModel>(question);
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.AnswerModel,
				AnswerViewModel>());
			viewQuestion.Answers = answers.Select(x => Mapper.Map<DataAccessLayer.Models.AnswerModel,
				AnswerViewModel>(x))
				.ToList();
			return PartialView("_QuestionDetailsPartial", viewQuestion);
		}

		public ActionResult Tests()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.TestModel, TestViewModel>());
			var testRepo = new TestRepository();
			var viewTests = testRepo.GetAllTests()
				.Select(x => Mapper.Map<DataAccessLayer.Models.TestModel, TestViewModel>(x))
				.ToList();
			return View(viewTests);
		}

		public ActionResult TestDetails(int testId)
		{
			var testRepo = new TestRepository();
			var test = testRepo.GetTestInfo(testId);

			if (test == null)
			{
				return HttpNotFound();
			}

			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.TestModel,
				TestDetailsViewModel>());
			var viewTest = Mapper.Map<DataAccessLayer.Models.TestModel,
				TestDetailsViewModel>(test);
			var questions = testRepo.GetTestQuestions(test.TestID);
			viewTest.Questions = new List<QuestionWithThemeViewModel>();
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.QuestionModel,
				QuestionWithThemeViewModel>());

			foreach (var question in questions)
			{
				var viewQuestion = Mapper.Map<DataAccessLayer.Models.QuestionModel,
					QuestionWithThemeViewModel>(question);
				viewQuestion.ThemeName = testRepo
					.GetThemeInfo(viewQuestion.ThemeID)
					.ThemeName;
				viewTest.Questions.Add(viewQuestion);
			}

			return View(viewTest);
		}

		public ActionResult DeleteTest(int testId)
		{
			var testRepo = new TestRepository();
			testRepo.DeleteTest(testId);
			return Json(string.Empty, JsonRequestBehavior.AllowGet);
		}

		public ActionResult CreateTest()
		{
			var testRepo = new TestRepository();
			var themes = testRepo.GetAllThemes();
			var questionsWithTheme = new List<QuestionWithThemeViewModel>();

			foreach (var theme in themes)
			{
				var questions = testRepo.GetThemeQuestions(theme.ThemeID);

				foreach (var question in questions)
				{
					questionsWithTheme.Add(new QuestionWithThemeViewModel
					{
						ThemeID = theme.ThemeID,
						ThemeName = theme.ThemeName,
						QuestionID = question.QuestionID,
						QuestionText = question.QuestionText,
						QuestionType = question.QuestionType
					});
				}
			}

			questionsWithTheme = questionsWithTheme
				.OrderBy(x => x.ThemeName)
				.ThenBy(x => x.QuestionText)
				.ToList();

			var creatingTest = new CreatingTestQuestionsViewModel
			{
				Questions = questionsWithTheme
			};

			return View(creatingTest);
		}

		[HttpPost]
		public ActionResult CreateTest(CreatingTestViewModel test)
		{
			if (test.Questions == null || test.Questions.Count < 1)
			{
				return Json(Resources.Resource.ERROR_TestMustContainQuestions);
			}

			var testRepo = new TestRepository();
			var creatingTest = new DataAccessLayer.Models.CreatingTestModel
			{
				TestName = test.TestName,
				TimeLimit = test.TimeLimit,
				AttemptsCount = test.AttemptsCount
			};
			var creatingTestId = testRepo.CreateTest(creatingTest);

			if (creatingTestId == TestRepository.DefaultErrorCode)
			{
				return Json(Resources.Resource.ERROR_TestExists);
			}

			foreach (var question in test.Questions)
			{
				var creatingTask = new DataAccessLayer.Models.CreatingTaskModel
				{
					TestID = creatingTestId,
					QuestionID = question
				};
				testRepo.CreateTask(creatingTask);
			}

			return Json(string.Empty);
		}
	}
}