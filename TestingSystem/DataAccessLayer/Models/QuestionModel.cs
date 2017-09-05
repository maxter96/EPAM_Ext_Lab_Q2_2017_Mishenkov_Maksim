namespace DataAccessLayer.Models
{
	public class QuestionModel
	{
		public int QuestionID { get; set; }

		public int ThemeID { get; set; }

		public string QuestionText { get; set; }

		public int QuestionType { get; set; }
	}
}
