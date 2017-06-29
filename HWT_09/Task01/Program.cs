namespace Task01
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowArray(int[] array)
		{
			if (array == null)
			{
				return;
			}

			foreach (int element in array)
			{
				Console.Write(element + " ");
			}

			Console.WriteLine();
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			int[] array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int sum = array.GetSum();

			Console.Write("Массив: ");
			ShowArray(array);
			Console.WriteLine("Сумма элементов: {0}", sum);
			Console.ReadKey();
		}
	}
}
