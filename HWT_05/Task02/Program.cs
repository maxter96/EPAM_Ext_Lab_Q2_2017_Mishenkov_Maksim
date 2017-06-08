namespace Task02
{
	using System;
	using System.Text;

	public class Program
	{
		private static void ShowData(Triangle triangle)
		{
			Console.WriteLine("Сторона A = {0:f4}", triangle.A);
			Console.WriteLine("Сторону Б = {0:f4}", triangle.B);
			Console.WriteLine("Сторону Б = {0:f4}", triangle.C);
			Console.WriteLine("Периметр  = {0:f4}", triangle.Perimeter);
			Console.WriteLine("Площадь   = {0:f4}\n", triangle.Area);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Console.WriteLine("Треугольник с конструктором без параметра:");
			var tr1 = new Triangle();
			ShowData(tr1);

			Console.WriteLine("Треугольник с конструктором с параметрами:");
			var tr2 = new Triangle(3, 4, 5);
			ShowData(tr2);

			Console.WriteLine("Треугольник с некорректными значениями:");
			var tr3 = new Triangle(3, 4, 8);
			ShowData(tr3);

			Console.ReadKey();
		}
	}
}
