namespace Task02
{
	using System;

	public delegate void SayHello(Person person, DateTime dateTime);

	public delegate void SayGoodBye(Person person);

	public class Office : IConsole
	{
		private SayHello sayHello;

		private SayGoodBye sayGoodBye;

		private event EventHandler<CameEventArgs> PersonCame;

		private event EventHandler PersonLeave;

		public Office()
		{
			PersonCame += OnPersonCame;
			PersonLeave += OnPersonLeave;
		}

		public void Came(Person person, DateTime dateTime)
		{
            ShowString(string.Format("[Пришел {0}. Время {1} часов]", person.Name, dateTime.Hour));

			if (PersonCame != null)
			{
				PersonCame(person, new CameEventArgs(dateTime));
			}

            ShowString(string.Empty);
        }

		public void Leave(Person person)
		{
			ShowString(string.Format("[Ушел {0}]", person.Name));

			if (PersonLeave != null)
			{
				PersonLeave(person, new EventArgs());
			}

            ShowString(string.Empty);

        }

		public void OnPersonCame(object sender, CameEventArgs args)
		{
			Person person = (Person)sender;

			if (sayHello != null)
			{
				sayHello(person, args.DateTime);
            }

			sayHello += person.SayHello;
			sayGoodBye += person.SayGoodbye;
		}

		public void OnPersonLeave(object sender, EventArgs args)
		{
			Person person = (Person)sender;
			sayHello -= person.SayHello;
			sayGoodBye -= person.SayGoodbye;

			if (sayGoodBye != null)
			{
                sayGoodBye(person);
            }
		}

        public void ShowString(string str)
        {
            Console.WriteLine(str);
        }
    }
}
