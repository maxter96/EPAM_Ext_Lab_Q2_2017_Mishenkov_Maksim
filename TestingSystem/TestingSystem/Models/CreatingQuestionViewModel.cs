using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class CreatingQuestionViewModel
	{
		public int ThemeID { get; set; }

		[Display(
			Name = "FIELD_QuestionText",
			ResourceType = typeof(Resources.Resource))]
		public string QuestionText { get; set; }

		public List<CreatingAnswerViewModel> Answers { get; set; }
	}
}