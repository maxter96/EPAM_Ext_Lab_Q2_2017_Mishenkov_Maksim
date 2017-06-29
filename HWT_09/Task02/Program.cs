namespace Task02
{
	using System;
	using System.Text;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			while (true)
			{
				Console.Clear();
				Console.Write("Введите число (для выхода - пустую строку): ");
				string input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return;
				}

				bool result = input.IsPositiveInteger();

				if (result == true)
				{
					Console.WriteLine("Это положительное целое число");
				}
				else
				{
					Console.WriteLine("Это не положительное целое число");
				}

				Console.ReadKey();
			}
		}
	}
}
