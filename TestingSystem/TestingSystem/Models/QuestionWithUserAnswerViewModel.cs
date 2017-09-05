using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class QuestionWithUserAnswerViewModel
	{
		public string QuestionText { get; set; }

		public List<UserAnswerViewModel> Answers { get; set; }
	}
}