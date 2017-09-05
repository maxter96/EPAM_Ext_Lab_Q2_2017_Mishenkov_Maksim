using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class UserRatingViewModel
	{
		public int UserID { get; set; }

		[Display(
			Name = "FIELD_UserName",
			ResourceType = typeof(Resources.Resource))]
		public string UserName { get; set; }

		[Display(
			Name = "FIELD_Score",
			ResourceType = typeof(Resources.Resource))]
		public int Score { get; set; }

		[Display(
			Name = "FIELD_QuestionsCount",
			ResourceType = typeof(Resources.Resource))]
		public int QuestionsCount { get; set; }
	}
}