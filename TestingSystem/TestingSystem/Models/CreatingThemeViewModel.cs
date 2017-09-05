using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace TestingSystem.Models
{
	public class CreatingThemeViewModel
	{
		[Required]
		[MaxLength(TestRepository.MaxThemeNameLength)]
		[Display(Name = "Название темы")]
		public string ThemeName { get; set; }
	}
}