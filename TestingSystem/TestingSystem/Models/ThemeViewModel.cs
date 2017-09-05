using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class ThemeViewModel
	{
		public int ThemeID { get; set; }

		[Display(
			Name = "FIELD_ThemeName",
			ResourceType = typeof(Resources.Resource))]
		public string ThemeName { get; set; }

		[Display(
			Name = "FIELD_QuestionsCount",
			ResourceType = typeof(Resources.Resource))]
		public int QuestionsCount { get; set; }
	}
}