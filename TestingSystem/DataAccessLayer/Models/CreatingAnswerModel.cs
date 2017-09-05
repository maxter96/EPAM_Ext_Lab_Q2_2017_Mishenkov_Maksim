namespace DataAccessLayer.Models
{
	public class CreatingAnswerModel
	{
		public int QuestionID { get; set; }

		public string AnswerText { get; set; }

		public bool IsCorrect { get; set; }
	}
}
