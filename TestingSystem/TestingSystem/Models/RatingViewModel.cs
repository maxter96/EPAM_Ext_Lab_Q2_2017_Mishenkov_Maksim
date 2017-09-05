using System.Collections.Generic;

namespace TestingSystem.Models
{
	public class RatingViewModel
	{
		public string RatingName { get; set; }

		public List<UserRatingViewModel> Users { get; set; }
	}
}