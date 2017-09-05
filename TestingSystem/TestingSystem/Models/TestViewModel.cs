using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
	public class TestViewModel
	{
		public int TestID { get; set; }

		[Display(
			Name = "FIELD_TestName",
			ResourceType = typeof(Resources.Resource))]
		public string TestName { get; set; }

		[Display(
			Name = "FIELD_TimeLimit",
			ResourceType = typeof(Resources.Resource))]
		public int TimeLimit { get; set; }

		[Display(
			Name = "FIELD_AttemptsCount",
			ResourceType = typeof(Resources.Resource))]
		public int AttemptsCount { get; set; }
	}
}