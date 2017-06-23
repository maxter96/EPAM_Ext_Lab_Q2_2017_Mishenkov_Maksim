namespace Task01
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading;

	public class Program
	{
		private static List<int> RemoveEvenNumbered(List<int> people)
		{
			var tempList = new List<int>();

			for (int i = 0; i < people.Count; i += 2)
			{
				tempList.Add(people[i]);
			}

			return tempList;
		}

		private static void ShowList(List<int> people)
		{
			foreach (int element in people)
			{
				Console.Write(element + " ");
			}

			Console.WriteLine();
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			while (true)
			{
				Console.Clear();
				Console.Write("Введте количество человек (пустой ввод для выхода) :  ");
				string input = Console.ReadLine();

				if (input == string.Empty)
				{
					break;
				}

				if (!int.TryParse(input, out int number))
				{
					Console.WriteLine("Некорректный ввод! Попробуйте еще раз.");
					Thread.Sleep(1500);
					continue;
				}

				if (number <= 0)
				{
					Console.WriteLine("Количество человек должно быть больше нуля!");
					Thread.Sleep(1500);
					continue;
				}

				var people = new List<int>();

				for (int i = 0; i < number; i++)
				{
					people.Add(i + 1);
				}

				while (people.Count > 1)
				{
					ShowList(people);
					people = RemoveEvenNumbered(people);
				}

				ShowList(people);

				Console.ReadKey();
			}
		}
	}
}
