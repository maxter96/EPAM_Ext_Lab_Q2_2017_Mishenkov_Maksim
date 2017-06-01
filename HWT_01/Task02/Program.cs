namespace Task02
{
	using System;
	using System.Threading;
	using System.Text;

	class Program
	{
		private static bool CalculateA(double h, out double result)
		{
			result = 0;
			try
			{
				double divident = Math.Abs(Math.Sin(8 * h)) + 17;
				double divider = Math.Pow(1 - ((Math.Sin(4 * h) * Math.Cos(Math.Pow(h, 2))) + 18), 2);
				double radical = divident / divider;
				result = Math.Sqrt(radical);
			}
			catch
			{
				return false;
			}

			return true;
		}

		private static bool CalculateB(double a, double h, out double result)
		{
			result = 0;
			try
			{
				double divident = 3;
				double divider = 3 + Math.Abs(Math.Tan(a * Math.Pow(h, 2)) - Math.Sin(a * h));
				double radical = divident / divider;
				result = 1 - Math.Sqrt(radical);
			}
			catch
			{
				return false;
			}

			return true;
		}

		private static bool CalculateC(double a, double b, double h, out double result)
		{
			result = 0;

			try
			{
				result = (a * Math.Pow(h, 2) * Math.Sin(b * h)) + (b * Math.Pow(h, 3) * Math.Cos(a * h));
			}
			catch
			{
				return false;
			}

			return true;
		}

		private static double CalculateDiscriminant(double a, double b, double c)
		{
			return Math.Pow(b, 2) - (4 * a * c);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			double h;

			while (true)
			{
				Console.Clear();
				Console.Write("Введите коэффициент h: ");
				string input = Console.ReadLine();

				if (!double.TryParse(input, out h))
				{
					Console.WriteLine("Некорректный ввод! Попробуйте еще раз!");
					Thread.Sleep(1500);
					continue;
				}

				break;
			}

			double a;

			if (!CalculateA(h, out a))
			{
				Console.WriteLine("Возникла ошибка при вычислении коэффициента A");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Значение коэффициента A = {0}", a);
			double b;

			if (!CalculateB(a, h, out b))
			{
				Console.WriteLine("Возникла ошибка при вычислении коэффициента B");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Значение коэффициента B = {0}", b);
			double c;

			if (!CalculateC(a, b, h, out c))
			{
				Console.WriteLine("Возникла ошибка при вычислении коэффициента C");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Значение коэффициента C = {0}", c);
			double discriminant = CalculateDiscriminant(a, b, c);
			Console.WriteLine("Значение дискриминанта = {0}\n", discriminant);

			if (discriminant < 0)
			{
				Console.WriteLine("Корней нет.");
				Console.ReadKey();
				return;
			}

			if (discriminant == 0)
			{
				var root = -b / (2 * a);
				Console.WriteLine("Корень уравнения: {0}", root);
				Console.ReadKey();
				return;
			}
			else
			{
				var firstRoot = (-b + Math.Sqrt(discriminant)) / (2 * a);
				var secondRoot = (-b - Math.Sqrt(discriminant)) / (2 * a);
				Console.WriteLine("Корни уравнения:\nX1 = {0}\nX2 = {1}", firstRoot, secondRoot);
				Console.ReadKey();
			}
		}
	}
}
