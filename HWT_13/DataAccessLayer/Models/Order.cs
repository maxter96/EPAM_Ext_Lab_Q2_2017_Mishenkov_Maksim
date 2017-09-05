namespace DataAccessLayer.Models
{
	using System;

	public class Order
	{
		public int OrderID { get; set; }

		public string CustomerID { get; set; }

		public int EmployeeID { get; set; }

		public string CompanyName { get; set; }

		public string EmployeeName { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? RequiredDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public OrderStatus Status { get; set; }

		public string ShipCountry { get; set; }

		public string ShipCity { get; set; }

		public string ShipAddress { get; set; }

		public string ShipName { get; set; }

		public float Sum { get; set; }
	}
}
