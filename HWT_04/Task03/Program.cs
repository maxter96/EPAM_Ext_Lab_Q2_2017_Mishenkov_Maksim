namespace Task03
{
	using System;
	using System.Diagnostics;
	using System.Text;

	// скриншот лежит в папке Screens

	public class Program
	{
		private static void WorkWithBuilder(int n)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < n; i++)
			{
				sb.Append("*");
			}
		}

		private static void WorkWithoutBuilder(int n)
		{
			string str = string.Empty;
			for (int i = 0; i < n; i++)
			{
				str += "*";
			}
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			int n = 100;
			var sw = new Stopwatch();
			WorkWithBuilder(n);
			WorkWithoutBuilder(n);

			Console.WriteLine("Результаты:");
			Console.WriteLine("{0,10}{1,15}{2,15}", "Размер", "With Builder", "Without");

			for (n = 100; n <= 1000; n += 100)//todo pn для чистоты эксперимента можно было ещё в разное время позапускать и показать, что выводит.
			{
				Console.Write("{0, 10}", n);
				sw.Restart();
				WorkWithBuilder(n);
				sw.Stop();
				Console.Write("{0, 15}", sw.Elapsed.TotalMilliseconds.ToString("0.0000"));

				sw.Restart();
				WorkWithoutBuilder(n);
				sw.Stop();
				Console.Write("{0, 15}\n", sw.Elapsed.TotalMilliseconds.ToString("0.0000"));
			}

			Console.ReadKey();
		}
	}
}
