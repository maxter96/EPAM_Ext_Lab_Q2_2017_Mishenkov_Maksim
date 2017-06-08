namespace Task04
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowString(string message, string str)
		{
			Console.WriteLine("{0,30}\"{1}\"\n", message.PadRight(30), str);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			MyString str = new MyString("   new");
			ShowString("Начальное состояние", str.ToString());

			str.Insert(6, new MyString("Str   "));
			ShowString("Вызов Insert(0,\'Str   \')", str.ToString());

			str.Remove(3, 3);
			ShowString("Вызов Remove(3,3)", str.ToString());

			str.Trim();
			ShowString("Вызов Trim()", str.ToString());

			str.ToLower();
			ShowString("Вызов ToLower()", str.ToString());

			str.ToUpper();
			ShowString("Вызов ToUpper()", str.ToString());

			str.Insert(0, new MyString("abcdeabcdeabcde"));
			ShowString("Вызов Insert", str.ToString());

			str.Replace('a', 'K');
			ShowString("Вызов Replace(\'a\', \'K\')", str.ToString());

			ShowString("Обращение к str[5]", str[5].ToString());
			ShowString("Вызов IndexOf(\'K\')", str.IndexOf('K').ToString());
			ShowString("Вызов LastIndexOf(\'K\')", str.LastIndexOf('K').ToString());
			ShowString("Вызов Contains(\'T\')", str.Contains('T').ToString());
			Console.ReadKey();
		}
	}
}
