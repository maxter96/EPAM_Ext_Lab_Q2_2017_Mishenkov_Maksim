using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class UserTestSessionResultViewModel
	{
		public int UserID { get; set; }

		public int TestID { get; set; }

		public string TestName { get; set; }

		public string Score { get; set; }

		public List<QuestonWithResultViewModel> Questions { get; set; }
	}
}