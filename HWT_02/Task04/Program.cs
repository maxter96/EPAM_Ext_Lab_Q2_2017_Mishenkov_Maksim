namespace Task04
{
	using System;
	using System.Text;
	using System.Threading;

	public class Program
	{
		private static void DrawPyramid(int n, int maxLevel)
		{
			StringBuilder spaces = new StringBuilder();
			StringBuilder stars = new StringBuilder("*");
			spaces.Append(new string(' ', maxLevel - 1));

			Console.WriteLine("{0}{1}", spaces.ToString(), stars);

			for (int i = 1; i < n; i++)
			{
				spaces.Remove(spaces.Length - 1, 1);
				stars.Append("**");
				Console.WriteLine("{0}{1}", spaces.ToString(), stars);
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			int maxLevel = 0;

			while (true)
			{
				Console.Clear();
				Console.Write("Введите кол-во пирамид: ");
				string input = Console.ReadLine();

				if (!int.TryParse(input, out maxLevel) || maxLevel <= 0)
				{
					Console.WriteLine("Некорректный ввод! Попробуйте снова.");
					Thread.Sleep(1500);
					continue;
				}

				break;
			}

			for (int i = 1; i <= maxLevel; i++)
			{
				DrawPyramid(i, maxLevel);
			}

			Console.ReadKey();
		}
	}
}
