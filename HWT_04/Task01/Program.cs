namespace Task01
{
	using System;
	using System.Text;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Console.WriteLine("Введите входную строку: ");
			StringBuilder builder = new StringBuilder(Console.ReadLine());

			for (int i = 0; i < builder.Length; i++)
			{
				if (char.IsPunctuation(builder[i]))
				{
					builder.Replace(builder[i], ' ');
				}
			}

			string[] data = builder.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			int count = data.Length;
			int sumLength = 0;
			double avgLength = 0;

			if (count != 0)
			{
				Console.WriteLine("\nСписок слов в строке:");
				foreach (var str in data)
				{
					Console.WriteLine(str);
					sumLength += str.Length;
				}

				avgLength = (double)sumLength / count;
			}
			else
			{
				Console.WriteLine("\nСлова в строке отсутствуют!");
			}

			Console.WriteLine("Средняя длина слова: {0}", avgLength);
			Console.ReadKey();
		}
	}
}
