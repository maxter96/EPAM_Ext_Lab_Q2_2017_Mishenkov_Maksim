using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataAccessLayer;

namespace WebApplication.Controllers
{
	public class HomeController : Controller//todo pn мог бы и переименовать)
	{
		public ActionResult Index()
		{
			var rep = new OrdersRepository();
			var orders = rep.GetAllOrders();
			var viewOrders = new List<Models.OrderViewModel>();
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.Order,
				Models.OrderViewModel>());

			foreach (var order in orders)
			{
				var viewOrder = Mapper.Map<DataAccessLayer.Models.Order,
					Models.OrderViewModel>(order);
				viewOrder.Sum = rep.GetOrderDetails(order.OrderID).Sum(x => x.Sum).ToString("F");//todo pn такое обычно на уровне базы делается. 
				viewOrders.Add(viewOrder);
			}

			return View(viewOrders);
		}

		public ActionResult Details(int? orderID)
		{
			if (!orderID.HasValue)
			{
				return RedirectToAction("Index");
			}

			var rep = new OrdersRepository();
			var order = rep.GetAllOrders()
				.Where(x => x.OrderID == orderID)
				.FirstOrDefault();

			if (order == null)
			{
				string message = "Заказа с таким ID не существует!";//todo pn в ресурсы
				return RedirectToAction("ErrorMessage", message);
			}

			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.Order,
				Models.FullOrderViewModel>());
			var viewOrder = Mapper.Map<DataAccessLayer.Models.Order,
				Models.FullOrderViewModel>(order);

			return View(viewOrder);
		}

		[ChildActionOnly]
		public ActionResult GetDetails(int orderID)
		{
			var rep = new OrdersRepository();
			var products = rep.GetOrderDetails(orderID);
			var viewProducts = new List<Models.DetailViewModel>();
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.OrderDetail,
				Models.DetailViewModel>()
				.ForMember("Sum", opt => opt.MapFrom(src => src.Sum.ToString("F"))));

			foreach (var product in products)
			{
				var viewProd = Mapper.Map<DataAccessLayer.Models.OrderDetail,
					Models.DetailViewModel>(product);
				viewProducts.Add(viewProd);
			}

			return PartialView("_Details", viewProducts);
		}

		[HttpPost]
		public ActionResult Delete(int[] orders)
		{
			var rep = new OrdersRepository();

			foreach (var order in orders)
			{
				rep.DeleteOrder(order);
			}

			return Json("Successful");//todo pn в enum
		}

		public ActionResult EditOrder(int? orderId)
		{
			var rep = new OrdersRepository();
			var viewOrder = new Models.CreatingOrderViewModel { IsNew = true };
			var customers = rep.GetAllCustomers();
			var employees = rep.GetAllEmployees();

			if (!orderId.HasValue)
			{
				ViewBag.Customers = customers;
				ViewBag.Employees = employees;
				return PartialView("_EditOrder", viewOrder);
			}

			var order = rep.GetAllOrders()
				.Where(x => x.OrderID == orderId.Value)
				.FirstOrDefault();

			if (order == null)
			{
				string message = "Заказа с таким ID не существует!";//todo pn в ресурсы
				return RedirectToAction("ErrorMessage", message);
			}

			ViewBag.Customers = customers
				.Where(x => x.CustomerID == order.CustomerID)
				.Concat(customers.Where(x => x.CustomerID != order.CustomerID));
			ViewBag.Employees = employees
				.Where(x => x.EmployeeID == order.EmployeeID)
				.Concat(employees.Where(x => x.EmployeeID != order.EmployeeID));
			Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.Order,
				Models.CreatingOrderViewModel>());
			viewOrder = Mapper.Map<DataAccessLayer.Models.Order,
				Models.CreatingOrderViewModel>(order);
			viewOrder.IsNew = false;

			return PartialView("_EditOrder", viewOrder);
		}

		[HttpPost]
		public ActionResult EditOrder(Models.CreatingOrderViewModel order)
		{
			var rep = new OrdersRepository();
			Mapper.Initialize(cfg => cfg.CreateMap<Models.CreatingOrderViewModel,
				DataAccessLayer.Models.CreatingOrder>());
			var creatingOrder = Mapper.Map<Models.CreatingOrderViewModel,
				DataAccessLayer.Models.CreatingOrder>(order);

			if (ModelState.IsValid)
			{
				if (order.IsNew)
				{
					rep.CreateOrder(creatingOrder);
				}
				else
				{
					rep.UpdateOrder(creatingOrder);
				}

				return RedirectToAction("Index");
			}

			string message = "При изменении/добавлении заказа возникла ошибка!";
			return RedirectToAction("ErrorMessage", message);
		}

		public JsonResult CheckCustomerID(string customerId)
		{
			var result = false;
			var rep = new OrdersRepository();
			var customer = rep.GetAllOrders()
				.Where(x => x.CustomerID == customerId)
				.FirstOrDefault();

			if (customer != null)
			{
				result = true;
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult CheckEmployeeID(int employeeId)
		{
			var result = false;
			var rep = new OrdersRepository();
			var employee = rep.GetAllOrders()
				.Where(x => x.EmployeeID == employeeId)
				.FirstOrDefault();

			if (employee != null)
			{
				result = true;
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult CreateProduct(int orderId)
		{
			var rep = new OrdersRepository();
			var products = rep.GetAllProducts();
			ViewBag.Products = products;
			var viewDetail = new Models.CreatingDetailViewModel { OrderID = orderId };
			return PartialView("_Product", viewDetail);
		}

		[HttpPost]
		public ActionResult CreateProduct(Models.CreatingDetailViewModel detail)
		{
			if (ModelState.IsValid)
			{
				var rep = new OrdersRepository();
				Mapper.Initialize(cfg => cfg.CreateMap<Models.CreatingDetailViewModel,
					DataAccessLayer.Models.CreatingOrderDetail>());
				var creatingDetail = Mapper.Map<Models.CreatingDetailViewModel,
					DataAccessLayer.Models.CreatingOrderDetail>(detail);
				rep.AddOrderDetail(creatingDetail);
				return RedirectToAction("Details", "Home", new { orderId = detail.OrderID });
			}

			string message = "При добавлении продукта возникла ошибка!";
			return RedirectToAction("ErrorMessage", message);
		}

		public ActionResult ErrorMessage(string message)
		{
			ViewBag.Message = message;
			return View();
		}
	}
}