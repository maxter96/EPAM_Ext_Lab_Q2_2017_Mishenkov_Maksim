namespace Task02
{
	using System;

	public class Program
	{
		private static void Main(string[] args)
		{
			Office office = new Office();
			Person tom = new Person("Tom");
			Person sam = new Person("Sam");
			Person peter = new Person("Peter");

			office.Came(tom, new DateTime(2017, 06, 24, 9, 0, 0));
			office.Came(peter, new DateTime(2017, 06, 24, 14, 0, 0));
			office.Came(sam, new DateTime(2017, 06, 24, 19, 0, 0));

			office.Leave(tom);
			office.Leave(peter);
			office.Leave(sam);

			Console.ReadKey();
		}
	}
}
