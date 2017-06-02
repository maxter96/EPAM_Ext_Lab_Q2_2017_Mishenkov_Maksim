namespace Task03
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowArray(int[] array)
		{
			int size = array.Length;
			for (int i = 0; i < size; i++)
			{
				Console.Write(array[i] + " ");
			}

			Console.WriteLine();
		}

		private static int GetSumOfNonNegative(int[] array)
		{
			int size = array.Length;
			int sum = 0;
			for (int i = 0; i < size; i++)
			{
				if (array[i] >= 0)
				{
					sum += array[i];
				}
			}

			return sum;
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Random random = new Random();
			int size = 10;
			int[] array = new int[size];

			for (int i = 0; i < size; i++)
			{
				array[i] = random.Next(-10, 10);
			}

			Console.Write("Исходный массив: ");
			ShowArray(array);
			Console.WriteLine("\nСумма неотрицательных элементов: {0}", GetSumOfNonNegative(array));
			Console.ReadKey();
		}
	}
}
