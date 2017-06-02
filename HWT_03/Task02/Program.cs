namespace Task02
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowArray(int[,,] array)
		{
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					for (int k = 0; k < array.GetLength(2); k++)
					{
						Console.WriteLine("[{0}][{1}][{2}]: {3}", i, j, k, array[i, j, k]);
					}
				}
			}

			Console.WriteLine();
		}

		private static void PositiveToZero(int[,,] array)
		{
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					for (int k = 0; k < array.GetLength(2); k++)
					{
						array[i, j, k] = array[i, j, k] > 0 ? 0 : array[i, j, k];
					}
				}
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			int size = 2;
			int[,,] array = new int[size, size, size];
			Random random = new Random();

			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					for (int k = 0; k < array.GetLength(2); k++)
					{
						array[i, j, k] = random.Next(-10, 10);
					}
				}
			}

			Console.WriteLine("Исходный массив:");
			ShowArray(array);
			PositiveToZero(array);
			Console.WriteLine("Массив после обнуления положительных значений:");
			ShowArray(array);
			Console.ReadKey();
		}
	}
}
