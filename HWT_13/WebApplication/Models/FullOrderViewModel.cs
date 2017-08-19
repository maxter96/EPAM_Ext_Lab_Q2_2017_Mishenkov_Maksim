using System.ComponentModel;

namespace WebApplication.Models
{
	public class FullOrderViewModel
	{
		[DisplayName("ID заказа")]
		public int OrderID { get; set; }

		[DisplayName("ID заказчика")]
		public string CustomerID { get; set; }

		[DisplayName("ID продавца")]
		public int EmployeeID { get; set; }

		[DisplayName("Компания")]
		public string CompanyName { get; set; }

		[DisplayName("Имя продавца")]
		public string EmployeeName { get; set; }

		[DisplayName("Дата отправки")]
		public string OrderDate { get; set; }

		[DisplayName("Крайний срок доставки")]
		public string RequiredDate { get; set; }

		[DisplayName("Дата доставки")]
		public string ShippedDate { get; set; }

		[DisplayName("Статус заказа")]
		public string Status { get; set; }

		[DisplayName("Страна")]
		public string ShipCountry { get; set; }

		[DisplayName("Город")]
		public string ShipCity { get; set; }

		[DisplayName("Адрес")]
		public string ShipAddress { get; set; }

		[DisplayName("Место")]
		public string ShipName { get; set; }
	}
}