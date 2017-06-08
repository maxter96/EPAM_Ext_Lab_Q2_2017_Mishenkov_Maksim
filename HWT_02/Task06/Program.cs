namespace Task06
{
	using System;
	using System.Text;
	using System.Threading;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;
			TextInfo info = new TextInfo();

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Параметры надписи: {0}", info.GetInfo());
				Console.WriteLine("Введите: ");

				int counter = 1;
				foreach (Parameter param in info.GetParameters())
				{
					Console.WriteLine("\t{0}: {1}", counter, param.Name);
					counter++;
				}

				Console.WriteLine("\t{0}: Очистить параметры", counter);
				Console.WriteLine("\t0: Выйти из программы");
				string input = Console.ReadLine();
				int menuIndex = 0;
				
				if (!int.TryParse(input, out menuIndex))
				{
					Console.WriteLine("Некорректный ввод! Попробуйте снова.");
					Thread.Sleep(1500);
					continue;
				}

				if (menuIndex == 0)
				{
					break;
				}

				if (menuIndex == counter)
				{
					info.ClearParameters();
					continue;
				}

				if (!info.ChangeParameter(menuIndex - 1))
				{
					Console.WriteLine("Пункта с таким индексом не существует!");
					Thread.Sleep(1500);
				}
			}
		}
	}
}
