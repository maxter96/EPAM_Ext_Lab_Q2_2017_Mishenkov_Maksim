using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class AnswerViewModel
	{
		public int AnswerID { get; set; }

		[Display(
			Name = "FIELD_AnswerText",
			ResourceType = typeof(Resources.Resource))]
		public string AnswerText { get; set; }

		[Display(
			Name = "FIELD_IsCorrect",
			ResourceType = typeof(Resources.Resource))]
		public bool IsCorrect { get; set; }
	}
}