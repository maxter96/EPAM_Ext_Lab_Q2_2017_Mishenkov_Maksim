using AutoMapper;
using DataAccessLayer;
using System.Web.Mvc;
using System.Web.Security;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public ActionResult LogOn()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Rating", "User");
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOn(UserPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Message = Resources.Resource.ERROR_WrongPassword;
				return View("ErrorView");
			}

			var userRepo = new UsersRepository();
			var user = userRepo.ValidateUser(model.Login, model.Password);

			if (user == null)
			{
				ViewBag.Message = Resources.Resource.ERROR_WrongPassword;
				return View("ErrorView");
			}

			FormsAuthentication.SetAuthCookie(user.Login, true);
			return RedirectToAction("Rating", "User");
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Rating", "User");
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(UserRegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Message = Resources.Resource.ERROR_IncorrectData;
				return View("ErrorView");
			}

			Mapper.Initialize(cfg => cfg.CreateMap<UserRegisterViewModel,
				DataAccessLayer.Models.CreatingUserModel>());

			var creatingUser = Mapper.Map<UserRegisterViewModel,
				DataAccessLayer.Models.CreatingUserModel>(model);

			var userRepo = new UsersRepository();
			var userID = userRepo.CreateUser(creatingUser);

			if (userID == UsersRepository.DefaultErrorCode)
			{
				ViewBag.Message = Resources.Resource.ERROR_LoginExists;
				return View("ErrorView");
			}

			var createdUser = userRepo.ValidateUser(model.Login, model.Password);
			userRepo.AddUserToRole(createdUser.Login, "User");
			FormsAuthentication.SetAuthCookie(createdUser.Login, true);
			return RedirectToAction("Rating", "User");
		}
	}
}