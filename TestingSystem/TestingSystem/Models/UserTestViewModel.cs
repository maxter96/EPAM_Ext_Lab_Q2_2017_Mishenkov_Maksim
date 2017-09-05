using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class UserTestViewModel
	{
		public int UserID { get; set; }

		public int TestID { get; set; }

		public string TestName { get; set; }

		public int LastScore { get; set; }

		public int QuestionsCount { get; set; }

		[Display(
			Name = "FIELD_RemainingAttempts",
			ResourceType = typeof(Resources.Resource))]
		public int RemainingAttempts { get; set; }
	}
}