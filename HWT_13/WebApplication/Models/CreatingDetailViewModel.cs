using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication.Properties;

namespace WebApplication.Models
{
	public class CreatingDetailViewModel
	{
		public int OrderID { get; set; }

		public int ProductID { get; set; }

		[DisplayName("Наименование")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		public string ProductName { get; set; }

		[DisplayName("Количество")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		[Range(
			1,
			20,
			ErrorMessageResourceName = "QuantityLimit",
			ErrorMessageResourceType = typeof(Resources))]
		public int Quantity { get; set; }

		[DisplayName("Скидка(%)")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		[Range(
			0,
			100,
			ErrorMessageResourceName = "DiscountLimit",
			ErrorMessageResourceType = typeof(Resources))]
		public int Discount { get; set; }
	}
}