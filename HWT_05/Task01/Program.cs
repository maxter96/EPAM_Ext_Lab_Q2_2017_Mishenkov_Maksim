namespace Task01
{
	using System;
	using System.Text;

	public class Program
	{
		public static void ShowData(Round round)
		{
			Console.WriteLine(
				"X={0:f3} Y ={1:f3} Радиус={2:f3} Площадь={3:f3}\n",
				round.X,
				round.Y,
				round.Radius,
				round.Area);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Console.WriteLine("Окружность с конструктором без параметра:");
			var round1 = new Round();
			ShowData(round1);

			Console.WriteLine("Окружность с конструктором с параметрами:");
			var round2 = new Round(7.5, 4, 12);
			ShowData(round2);

			Console.WriteLine("Окружность с некорректным значением радиуса:");
			var round3 = new Round(7.5, 4, -4);
			ShowData(round3);

			Console.ReadKey();
		}
	}
}
