using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebApplication.Properties;

namespace WebApplication.Models
{
	public class CreatingOrderViewModel
	{
		public int OrderID { get; set; }

		[Remote(
			"CheckCustomerID",
			"Home",
			ErrorMessageResourceName = "EmptyCustomerId",
			ErrorMessageResourceType = typeof(Resources))]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		[DisplayName("ID заказчика")]
		[StringLength(
			10,
			MinimumLength = 3,
			ErrorMessageResourceName = "CustomerLengthLimit",
			ErrorMessageResourceType = typeof(Resources))]
		public string CustomerID { get; set; }

		[Remote(
			"CheckEmployeeID",
			"Home",
			ErrorMessageResourceName = "EmptyEmployeeId",
			ErrorMessageResourceType = typeof(Resources))]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		[DisplayName("ID продавца")]
		public int EmployeeID { get; set; }

		[DataType(
			DataType.Date,
			ErrorMessageResourceName = "WrongDate",
			ErrorMessageResourceType = typeof(Resources))]
		[DisplayName("Дата отправки")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		public DateTime OrderDate { get; set; }

		[DataType(
			DataType.Date,
			ErrorMessageResourceName = "WrongDate",
			ErrorMessageResourceType = typeof(Resources))]
		[DisplayName("Крайний срок доставки")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		public DateTime RequiredDate { get; set; }

		[DataType(
			DataType.Date,
			ErrorMessageResourceName = "WrongDate",
			ErrorMessageResourceType = typeof(Resources))]
		[DisplayName("Дата доставки")]
		[Required(
			ErrorMessageResourceName = "EmptyField",
			ErrorMessageResourceType = typeof(Resources))]
		public DateTime ShippedDate { get; set; }

		[Display(AutoGenerateField = false)]
		public bool IsNew { get; set; }
	}
}