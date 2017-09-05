namespace DataAccessLayer.Models
{
	public class OrderDetail
	{
		public int OrderID { get; set; }

		public int ProductID { get; set; }

		public string ProductName { get; set; }

		public float Sum { get; set; }
	}
}
