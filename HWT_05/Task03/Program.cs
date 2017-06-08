namespace Task03
{
	using System;
	using System.Text;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			var user1 = new User("Мишенков", "Максим", "Андреевич", new DateTime(1996, 9, 29));
			Console.WriteLine(user1.ToString());
			user1.DateOfBirth = new DateTime(1990, 1, 1);
			Console.WriteLine(user1.ToString());

			// Эти строки вызовут исключение
			// user1.Name = null;
			// user1.SoName = "";
			Console.ReadKey();
		}
	}
}
