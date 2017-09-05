using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class RunningTestViewModel
	{
		public int UserID { get; set; }

		public int TestSessionID { get; set; }

		public string TestName { get; set; }

		public List<QuestionWithUserAnswerViewModel> Questions { get; set; }
	}
}