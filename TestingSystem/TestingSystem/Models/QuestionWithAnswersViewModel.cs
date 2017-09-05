using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class QuestionWithAnswersViewModel : QuestionViewModel
	{
		public List<AnswerViewModel> Answers { get; set; }
	}
}