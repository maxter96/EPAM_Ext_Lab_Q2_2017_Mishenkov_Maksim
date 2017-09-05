using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class QuestionWithThemeViewModel : QuestionViewModel
	{
		[Display(
			Name = "FIELD_ThemeName",
			ResourceType = typeof(Resources.Resource))]
		public string ThemeName { get; set; }
	}
}