using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace TestingSystem.Models
{
	public class UserRegisterViewModel
	{
		[Required]
		[MaxLength(UsersRepository.MaxLoginLength)]
		[MinLength(5)]
		[Display(
			Name = "FIELD_Login",
			ResourceType = typeof(Resources.Resource))]
		public string Login { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MaxLength(UsersRepository.MaxPasswordLength)]
		[MinLength(5)]
		[Display(
			Name = "FIELD_Password",
			ResourceType = typeof(Resources.Resource))]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]
		[Display(
			Name = "FIELD_RepeatPassword",
			ResourceType = typeof(Resources.Resource))]
		public string ConfirmPassword { get; set; }

		[Required]
		[MaxLength(UsersRepository.MaxFirstNameLength)]
		[MinLength(3)]
		[Display(
			Name = "FIELD_FirstName",
			ResourceType = typeof(Resources.Resource))]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(UsersRepository.MaxLastNameLength)]
		[MinLength(3)]
		[Display(
			Name = "FIELD_LastName",
			ResourceType = typeof(Resources.Resource))]
		public string LastName { get; set; }
	}
}