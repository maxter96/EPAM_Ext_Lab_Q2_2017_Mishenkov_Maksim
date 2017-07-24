namespace DataAccessLayer.Models
{
	using System;

	public class Order
	{
		public int OrderID { get; set; }

		public string CustomerID { get; set; }

		public int EmployeeID { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public OrderStatus Status { get; set; }
	}
}
