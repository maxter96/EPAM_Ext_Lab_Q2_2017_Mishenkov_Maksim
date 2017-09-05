using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace TestingSystem.Models
{
	public class UserPasswordViewModel
	{
		[Display(
			Name = "FIELD_Login",
			ResourceType = typeof(Resources.Resource))]
		[Required]
		[MaxLength(UsersRepository.MaxLoginLength)]
		[MinLength(6)]
		public string Login { get; set; }

		[Display(
			Name = "FIELD_Password",
			ResourceType = typeof(Resources.Resource))]
		[Required]
		[MaxLength(UsersRepository.MaxPasswordLength)]
		[DataType(DataType.Password)]
		[MinLength(6)]
		public string Password { get; set; }
	}
}