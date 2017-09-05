using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class UserResultsViewModel
	{
		public string UserName { get; set; }

		public List<UserTestViewModel> Tests { get; set; }
	}
}