using System.ComponentModel;

namespace WebApplication.Models
{
	public class DetailViewModel
	{
		[DisplayName("ID")]
		public int ProductID { get; set; }

		[DisplayName("Наименование")]
		public string ProductName { get; set; }

		[DisplayName("Сумма")]
		public string Sum { get; set; }
	}
}