using System.Web.Mvc;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}