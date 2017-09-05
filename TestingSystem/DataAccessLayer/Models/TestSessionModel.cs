using System;

namespace DataAccessLayer.Models
{
	public class TestSessionModel
	{
		public int TestSessionID { get; set; }

		public int UserID { get; set; }

		public int TestID { get; set; }

		public DateTime BeginTime { get; set; }

		public DateTime EndTime { get; set; }

		public DateTime? Time { get; set; }

		public int QuestionsCount { get; set; }

		public int RightAnswers { get; set; }
	}
}
