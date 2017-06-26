namespace Task02
{
	using System;

	public class Person
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
				Console.WriteLine("\'Доброе утро, {0}!\' - сказал {1}", person.Name, this.Name);//todo pn в отдельном классе не должно быть зависимостей от других классов (в твоём случае от класса вывода данных)
			}
			else if (time.Hour < EveningTime)
			{
				Console.WriteLine("\'Добрый день, {0}!\' - сказал {1}", person.Name, this.Name);
			}
			else
			{
				Console.WriteLine("\'Добрый вечер, {0}!\' - сказал {1}", person.Name, this.Name);
			}
		}

		public void SayGoodbye(Person person)
		{
			Console.WriteLine("\'До свидания, {0}!\' - сказал {1}", person.Name, this.Name);
		}
	}
}
