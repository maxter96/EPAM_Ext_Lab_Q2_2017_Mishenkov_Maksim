namespace Task02
{
	using System;
	using System.Text;
	using System.Threading;

	public class Program
	{
		private static void DrawTriangle(int n)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 1; i <= n; i++)
			{
				builder.Append("*");
				Console.WriteLine(builder.ToString());
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			int n = 0;

			while (true)
			{
				Console.Clear();
				Console.Write("Введите кол-во строк: ");
				string input = Console.ReadLine();

				if (!int.TryParse(input, out n) || n <= 0)
				{
					Console.WriteLine("Некорректный ввод! Попробуйте снова.");
					Thread.Sleep(1500);
					continue;
				}

				break;
			}

			DrawTriangle(n);
			Console.ReadKey();
		}
	}
}
