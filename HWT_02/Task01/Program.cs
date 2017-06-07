namespace Task01
{
	using System;
	using System.Text;
	using System.Threading;

	public class Program
	{
		public static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			int a = 0;
			int b = 0;

			while (true)
			{
				Console.Clear();
				Console.Write("Введите длины сторон прямоугольника, разделенные пробелом: ");
				string input = Console.ReadLine();
				string[] data = input.Split(' ');

				a = int.Parse(data[0]);//todo pn упадет здесь, если пользователь введет не число
				b = int.Parse(data[1]);

				if (a <= 0 || b <= 0)
				{
					Console.WriteLine("Длины сторон должны быть положительными! Введите снова.");
					Thread.Sleep(1500);
					continue;
				}

				break;
			}

			int square = a * b;
			Console.WriteLine("Площадь прямоугольника со сторонами {0} и {1} равна {2}", a, b, square);
			Console.ReadKey();
		}
	}
}
