namespace DataAccessLayer.Models
{
	public class AnswerModel
	{
		public int AnswerID { get; set; }

		public string AnswerText { get; set; }

		public bool IsCorrect { get; set; }

		public int QuestionID { get; set; }
	}
}
