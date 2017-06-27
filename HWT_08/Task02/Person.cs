namespace Task02
{
	using System;

	public class Person : IConsole
	{
		private const int MorningTime = 12;
		private const int EveningTime = 17;
		private string name;

		public Person(string name)
		{
			this.name = name;
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public void SayHello(Person person, DateTime time)
		{
			if (time.Hour < MorningTime)
			{
				ShowString(string.Format("\'Доброе утро, {0}!\' - сказал {1}", person.Name, this.Name));
			}
			else if (time.Hour < EveningTime)
			{
                ShowString(string.Format("\'Добрый день, {0}!\' - сказал {1}", person.Name, this.Name));
			}
			else
			{
                ShowString(string.Format("\'Добрый вечер, {0}!\' - сказал {1}", person.Name, this.Name));
			}
		}

		public void SayGoodbye(Person person)
		{
            ShowString(string.Format("\'До свидания, {0}!\' - сказал {1}", person.Name, this.Name));
		}

        public void ShowString(string str)
        {
            Console.WriteLine(str);
        }
    }
}
