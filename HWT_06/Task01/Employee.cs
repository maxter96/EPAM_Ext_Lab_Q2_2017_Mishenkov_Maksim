using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Task01
{
	class Employee : User
	{

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
            var type = typeof(Post);
            var fieldInfo = type.GetField(this.Post.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var strPost = attributes[0].Description;
			return base.ToString() + string.Format("\nСтаж (в годах): {0}; Должность: {1}.", WorkExperience, strPost);
		}
	}
}
