namespace Task03
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class Program
	{
		private static void ShowArray<T>(string message, DynamicArray<T> array) where T : IComparable<T>
		{
			Console.WriteLine(message);

			foreach (T element in array)
			{
				Console.Write(element + " ");
			}

			Console.WriteLine("\nLength: {0}", array.Length);
			Console.WriteLine("Capacity: {0}\n", array.Capacity);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			Console.WindowHeight = 30;

			var arr = new DynamicArray<int>();
			arr.Add(1);
			arr.Add(2);
			arr.Add(3);
			ShowArray("Добавление одиночных элементов", arr);

			var range = new List<int> { 4, 5, 6, 7, 8, 9, 10 };
			arr.AddRange(range);
			ShowArray("Добавление IEnumerable", arr);

			for (int i = 0; i <= 10; i+=2)
			{
				arr.Remove(i + 1);
			}

			ShowArray("Удаление элементов", arr);

			for (int i = 0; i < 10; i += 2)
			{
				arr.Insert(i, i+1);
			}

			ShowArray("Вставка элементов", arr);

			arr.AddRange(range);
			arr.AddRange(range);
			arr.AddRange(range);
			arr.AddRange(range);
			arr.Remove(1);

			ShowArray("Вставка большого числа элементов", arr);

			Console.WriteLine("Последний элемент массива: {0}", arr[arr.Length - 1]);

			Console.ReadKey();
		}
	}
}
