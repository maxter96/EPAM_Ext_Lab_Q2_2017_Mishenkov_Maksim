namespace Task04
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowArray(int[,] array)
		{
			Console.Write("[{0,3}]", string.Empty);
			for (int i = 0; i < array.GetLength(1); i++)
			{
				Console.Write("[{0,3}]", i);
			}

			Console.WriteLine("\n");

			for (int i = 0; i < array.GetLength(0); i++)
			{
				Console.Write("[{0,3}]", i);
				for (int j = 0; j < array.GetLength(1); j++)
				{
					Console.Write("{0,5}", array[i, j]);
				}

				Console.WriteLine("\n");
			}

			Console.WriteLine();
		}

		private static int GetSumOfEvenNumbered(int[,] array)
		{
			int sum = 0;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					if ((i + j) % 2 == 0)
					{
						sum += array[i, j];
					}
				}
			}

			return sum;
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Random random = new Random();
			int size = 4;
			int[,] array = new int[size, size];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					array[i, j] = random.Next(6);
				}
			}

			Console.WriteLine("Исходный массив:\n");
			ShowArray(array);
			int sum = GetSumOfEvenNumbered(array);
			Console.WriteLine("Сумма элементов, стоящих на четной позиции: {0}", sum);
			Console.ReadKey();
		}
	}
}
