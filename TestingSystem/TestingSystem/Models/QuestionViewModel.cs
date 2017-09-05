using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class QuestionViewModel
	{
		[Display(AutoGenerateField = false)]
		public int QuestionID { get; set; }

		[Display(AutoGenerateField = false)]
		public int ThemeID { get; set; }

		[Display(
			Name = "FIELD_QuestionText",
			ResourceType = typeof(Resources.Resource))]
		public string QuestionText { get; set; }

		[Display(
			Name = "FIELD_QuestionType",
			ResourceType = typeof(Resources.Resource))]
		public int QuestionType { get; set; }
	}
}