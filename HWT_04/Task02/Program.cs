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

			Console.Write("Введите первую строку: ");
			string firstString = Console.ReadLine();
			Console.Write("Введите вторую строку: ");
			string secondString = Console.ReadLine();
			StringBuilder data = new StringBuilder();

			for (int i = 0; i < firstString.Length; i++)
			{
				data.Append(firstString[i]);

				if (secondString.Contains(firstString[i].ToString()))
				{
					data.Append(firstString[i]);
				}
			}

			Console.Write("Рузельтирующая строка: ");
			Console.WriteLine(data.ToString());
			Console.ReadKey();
		}
	}
}
