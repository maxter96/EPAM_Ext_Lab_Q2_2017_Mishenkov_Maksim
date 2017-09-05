using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class CreatingTestViewModel
	{
		public string TestName { get; set; }

		public int TimeLimit { get; set; }

		public int AttemptsCount { get; set; }

		public List<int> Questions { get; set; }
	}
}