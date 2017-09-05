using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class UserTestWithSessionsViewModel
	{
		public int UserID { get; set; }

		public int TestID { get; set; }

		public string TestName { get; set; }

		public int AttemptsCount { get; set; }

		public int AttemptsLeft { get; set; }

		public List<UserAttemptViewModel> Sessions { get; set; }
	}
}