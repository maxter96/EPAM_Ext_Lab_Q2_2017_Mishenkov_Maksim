using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
	public class CreatingDetailViewModel
	{
		public int OrderID { get; set; }

		public int ProductID { get; set; }

		[DisplayName("Наименование")]
		public string ProductName { get; set; }

		[DisplayName("Количество")]
		[Range(1, 20, ErrorMessage = "Количество должно быть от 1 до 20")]//todo pn сообщение в ресурсы
		public int Quantity { get; set; }

		[DisplayName("Скидка")]
		[Range(0.0f, 1.0f, ErrorMessage = "Скидка должна быть от 0.0 до 0.1!")]//todo pn сообщение в ресурсы
		public decimal Discount { get; set; }
	}
}