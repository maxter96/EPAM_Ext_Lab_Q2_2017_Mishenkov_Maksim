namespace Task03
{
	using System;
	using System.Text;
	using System.Threading;

	public class Program
	{
		private static void DrawPyramid(int n)
		{
			StringBuilder spaces = new StringBuilder();
			StringBuilder stars = new StringBuilder("*");
			spaces.Append(new string(' ', n - 1));

			Console.WriteLine("{0}{1}", spaces.ToString(), stars.ToString());//todo pn ToString здесь не нужен

			for (int i = 1; i < n; i++)
			{
				spaces.Remove(spaces.Length - 1, 1);
				stars.Append("**");
				Console.WriteLine("{0}{1}", spaces.ToString(), stars.ToString());//todo pn ToString здесь не нужен
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

			DrawPyramid(n);
			Console.ReadKey();
		}
	}
}
