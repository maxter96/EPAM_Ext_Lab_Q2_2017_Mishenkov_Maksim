namespace Task01
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

		private static void SortArray(int[] array)
		{
			int size = array.Length;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size - 1; j++)
				{
					if (array[j + 1] < array[j])
					{
						int temp = array[j];
						array[j] = array[j + 1];
						array[j + 1] = temp;
					}
				}
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			int size = 20;
			int[] numbers = new int[size];
			Random random = new Random();

			for (int i = 0; i < size; i++)
			{
				numbers[i] = random.Next(-50, 50);
			}

			Console.WriteLine("Исходный массив: ");
			ShowArray(numbers);
			SortArray(numbers);
			Console.WriteLine("\nОтсортированный массив: ");
			ShowArray(numbers);
			Console.WriteLine("\nМинимум: {0}", numbers[0]);
			Console.WriteLine("\nМаксимум: {0}", numbers[numbers.Length - 1]);
			Console.ReadKey();
		}
	}
}
