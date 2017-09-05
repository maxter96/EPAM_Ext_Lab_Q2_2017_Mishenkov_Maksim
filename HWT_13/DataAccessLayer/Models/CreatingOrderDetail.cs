namespace DataAccessLayer.Models
{
	public class CreatingOrderDetail
	{
		public int OrderID { get; set; }

		public int ProductID { get; set; }

		public float Discount { get; set; }

		public int Quantity { get; set; }
	}
}
