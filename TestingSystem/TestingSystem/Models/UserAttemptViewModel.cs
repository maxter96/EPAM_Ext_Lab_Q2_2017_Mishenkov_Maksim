using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class UserAttemptViewModel
	{
		public int UserID { get; set; }

		public int TestID { get; set; }

		public int TestSessionID { get; set; }

		[Display(
			Name = "FIELD_AttemptNumber",
			ResourceType = typeof(Resources.Resource))]
		public int AttemptNumber { get; set; }

		[Display(
			Name = "FIELD_Score",
			ResourceType = typeof(Resources.Resource))]
		public string Score { get; set; }

		[Display(
			Name = "FIELD_Time",
			ResourceType = typeof(Resources.Resource))]
		public int Time { get; set; }
	}
}