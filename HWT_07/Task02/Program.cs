namespace Task02
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class Program //todo pn ну, "частота встречаемости" имелось в виду написать количество вхождений каждого слова, но пусть будет как ты реализовал.
	{
		private static string[] ParseText(string text)
		{
			StringBuilder builder = new StringBuilder(text);

			for (int i = 0; i < builder.Length; i++)
			{
				if (char.IsPunctuation(builder[i]))
				{
					builder.Replace(builder[i], ' ');
				}
			}

			return builder.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		}

		private static SortedDictionary<string, double> GetFrequency(string[] words)
		{
			var frequency = new SortedDictionary<string, double>();
			int count = words.Length;

			if (count == 0)
			{
				return frequency;
			}

			double oneWordProbability = 1.0 / count;

			foreach (string word in words)
			{
				string wordInLower = word.ToLower();

				if (!frequency.ContainsKey(wordInLower))
				{
					frequency[wordInLower] = 0;
				}

				frequency[wordInLower] += oneWordProbability;
			}

			return frequency;
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Введите текст (пустой ввод для выхода):\n");

				string input = Console.ReadLine();

				if (input == string.Empty)
				{
					break;
				}

				string[] words = ParseText(input);
				var frequency = GetFrequency(words);

				Console.WriteLine("\nСлова и частота их встречаемости:\n");

				foreach (string key in frequency.Keys)
				{
					Console.WriteLine("{0,15} {1:f3}", key.PadRight(15), frequency[key]);
				}

				Console.ReadKey();
			}
		}
	}
}
