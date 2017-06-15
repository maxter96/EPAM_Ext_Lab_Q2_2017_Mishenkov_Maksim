using System;
using System.Collections.Generic;

namespace Task01
{
	class Employee : User
	{
		public static Dictionary<Post, string> Posts = new Dictionary<Post, string>
		{
			[Post.GeneralDirector] = "Генеральный директор",
			[Post.Analyst] = "Аналитик",
			[Post.ProjectManager] = "Менеджер проекта",
			[Post.SoftwareTester] = "Тестировщик",
			[Post.SoftwareEngineer] = "Разработчик"
		};

		private int workExperience;

		public Employee(string soName, string name, string patronymic, DateTime dateOfBirth, int workExperience, Post post) 
			: base(soName, name, patronymic, dateOfBirth)
		{
			WorkExperience = workExperience;
			Post = post;
		}

		public Post Post { get; set; }

		public int WorkExperience
		{
			get
			{
				return workExperience;
			}
			set
			{
				if (value < 0)
				{
					throw new FormatException("Стаж работы не может быть отрицательным!");
				}

				if (value > Age)
				{
					throw new FormatException("Стаж работы не может быть больше возраста!");
				}

				workExperience = value;
			}
		}

		public override string ToString()
		{
			if (!Employee.Posts.ContainsKey(Post))
				throw new Exception("Неизвестная должность!");
			string post = Employee.Posts[Post];
			return base.ToString() + string.Format("\nСтаж (в годах): {0}; Должность: {1}.", workExperience, post);
		}
	}
}
