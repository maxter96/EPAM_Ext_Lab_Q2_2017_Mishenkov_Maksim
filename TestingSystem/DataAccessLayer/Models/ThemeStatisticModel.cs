namespace DataAccessLayer.Models
{
	public class ThemeStatisticModel
	{
		public int ThemeStatisticID { get; set; }

		public int TestSessionID { get; set; }

		public int ThemeID { get; set; }

		public int RightAnswers { get; set; }

		public int QuestionsCount { get; set; }
	}
}
