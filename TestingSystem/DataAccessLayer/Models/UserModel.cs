using System;

namespace DataAccessLayer.Models
{
	public class UserModel
	{
		public int UserID { get; set; }

		public string Login { get; set; }

		public Guid Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
