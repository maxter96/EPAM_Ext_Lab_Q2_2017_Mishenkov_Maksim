namespace Task01
{
	using System;

	public class User
	{
		private string name;
		private string soName;
		private string patronymic;

		public User(string soName, string name, string patronymic, DateTime dateOfBirth)
		{
			Name = name;
			SoName = soName;
			Patronymic = patronymic;
			DateOfBirth = dateOfBirth;
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new FormatException("Имя не может быть пустым");
				}
				else
				{
					name = value;
				}
			}
		}

		public string SoName
		{
			get
			{
				return soName;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new FormatException("Фамилия не может быть пустой");
				}
				else
				{
					soName = value;
				}
			}
		}

		public string Patronymic
		{
			get
			{
				return patronymic;
			}

			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new FormatException("Отчество не может быть пустым");
				}
				else
				{
					patronymic = value;
				}
			}
		}

		public DateTime DateOfBirth { get; set; }

		public int Age
		{
			get
			{
				DateTime now = DateTime.Now;
				int age = now.Year - DateOfBirth.Year;

				if (DateOfBirth.AddYears(age) > now)
				{
					return age - 1;
				}

				return age;
			}
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}; Возраст: {3}", SoName, Name, Patronymic, Age);
		}
	}
}
