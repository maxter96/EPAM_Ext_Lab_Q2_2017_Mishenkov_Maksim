using System;

namespace DataAccessLayer.Models
{
	public class CreatingOrder
	{
		public int OrderID { get; set; }

		public string CustomerID { get; set; }

		public int EmployeeID { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime RequiredDate { get; set; }

		public DateTime ShippedDate { get; set; }
	}
}
