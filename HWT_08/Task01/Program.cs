namespace Task01
{
	using System;
	using System.Text;

	public class Program
	{
		private delegate int Compare(string str1, string str2);

		private static int CompareStrings(string str1, string str2)
		{
			int lengthComparison = str2.Length.CompareTo(str1.Length);

			if (lengthComparison != 0)
			{
				return lengthComparison;
			}

			return str2.CompareTo(str1);
		}

		private static void SortStrings(string[] array, Compare compare)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 1; j < array.Length; j++)
				{
					if (compare(array[j - 1], array[j]) < 0)
					{
						string temp = array[j - 1];
						array[j - 1] = array[j];
						array[j] = temp;
					}
				}
			}
		}

		private static void ShowStrings(string[] strings)
		{
			Console.WriteLine("Строки после сортировки:");
			foreach (string str in strings)
			{
				Console.WriteLine(str);
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Введите слова (для окончания ввода - пустая строка)\n");

                string input = Console.ReadLine();

				if (input == string.Empty)
				{
					return;
				}

				string[] array = input.Split(' ');
				SortStrings(array, CompareStrings);
				ShowStrings(array);
				Console.ReadKey();
			}
		}
	}
}
