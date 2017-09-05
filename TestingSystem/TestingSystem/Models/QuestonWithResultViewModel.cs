using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class QuestonWithResultViewModel
	{
		public string QuestionText { get; set; }

		public int Points { get; set; }

		public List<UserAnswerViewModel> Answers { get; set; }
	}
}