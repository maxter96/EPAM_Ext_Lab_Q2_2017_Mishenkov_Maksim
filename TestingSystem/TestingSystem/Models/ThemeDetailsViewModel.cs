using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class ThemeDetailsViewModel
	{
		[Display(AutoGenerateField = false)]
		public int ThemeID { get; set; }

		[Display(Name = "Название темы")]
		public string ThemeName { get; set; }

		[Display(Name = "Количество вопросов")]
		public int QuestionsCount { get; set; }

		public List<QuestionViewModel> Questions { get; set; }
	}
}