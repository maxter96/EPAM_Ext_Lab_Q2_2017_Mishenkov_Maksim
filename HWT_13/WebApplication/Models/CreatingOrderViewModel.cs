using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models
{
	public class CreatingOrderViewModel
	{
		public int OrderID { get; set; }

		[Remote("CheckCustomerID", "Home", ErrorMessage = "Такого покупателя не существует")]
		[Required(ErrorMessage = "Поле не должно быть пустым!")]
		[DisplayName("ID заказчика")]
		[StringLength(10, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 10 символов!")]
		public string CustomerID { get; set; }

		[Remote("CheckEmployeeID", "Home", ErrorMessage = "Такого продавца не существует")]
		[Required(ErrorMessage = "Введите число!")]
		[DisplayName("ID продавца")]
		public int EmployeeID { get; set; }

		[Display(AutoGenerateField = false)]
		public bool IsNew { get; set; }
	}
}