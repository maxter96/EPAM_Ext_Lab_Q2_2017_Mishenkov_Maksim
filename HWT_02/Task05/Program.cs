namespace Task05
{
	using System;
	using System.Text;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			int sum = 0;

			for (int i = 1; i < 1000; i++)
			{
				if (i % 3 == 0 || i % 5 == 0)
				{
					sum += i;
				}
			}

			Console.WriteLine("Сумма всех натуральных чисел меньше 1000 кратных 3 и 5 равна {0}", sum);
			Console.ReadKey();
		}
	}
}
