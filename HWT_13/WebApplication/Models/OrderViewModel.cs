using System.ComponentModel;

namespace WebApplication.Models
{
	public class OrderViewModel
	{
		[DisplayName("ID")]
		public int OrderID { get; set; }

		[DisplayName("Заказчик")]
		public string CompanyName { get; set; }

		[DisplayName("Статус")]
		public string Status { get; set; }

		[DisplayName("Дата")]
		public string OrderDate { get; set; }

		[DisplayName("Сумма")]
		public string Sum { get; set; }
	}
}