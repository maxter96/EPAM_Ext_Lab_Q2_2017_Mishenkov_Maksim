namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public class Program
    {
        private static List<IDrawable> objects;

		private static void CreateObject(int type, double[] parameters)
		{
			switch (type)
			{
				case 0:
					objects.Add(new Line(parameters[0], parameters[1], parameters[2], parameters[3]));
					break;
				case 1:
					objects.Add(new Rectangle(parameters[0], parameters[1], parameters[2], parameters[3]));
					break;
				case 2:
					objects.Add(new Circle(parameters[0], parameters[1], parameters[2]));
					break;
				case 3:
					objects.Add(new Round(parameters[0], parameters[1], parameters[2]));
					break;
				case 4:
					objects.Add(new Ring(parameters[0], parameters[1], parameters[2], parameters[3]));
					break;
			}
		}

        private static void AddObject(string message, string paramString, int type, int numberOfParams)
        {
            double[] parameters = new double[numberOfParams];
            while (true)
            {
                Console.Clear();
                Console.WriteLine(message);
                Console.WriteLine("Введите " + paramString + " через пробел:");
                string input = Console.ReadLine();
                string[] data = input.Split(' ');

                try
                {
                    for (int i = 0; i < numberOfParams; i++)
                    {
                        parameters[i] = double.Parse(data[i]);
                    }

                    break;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод! Попробуйте снова.");
                    Thread.Sleep(2000);
                    continue;
                }
            }

			CreateObject(type, parameters);
            Thread.Sleep(2000);
        }

        private static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WindowWidth = 150;
            objects = new List<IDrawable>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Список объектов:");
                foreach (IDrawable obj in objects)
                {
                    obj.ShowInfo();
                }

                Console.WriteLine("\nСписок доступных команд:");
                Console.WriteLine("1 - Добавить линию;");
                Console.WriteLine("2 - Добавить прямоугольник;");
                Console.WriteLine("3 - Добавить окружность;");
                Console.WriteLine("4 - Добавить круг;");
                Console.WriteLine("5 - Добавить кольцо;");
                Console.WriteLine("0 - Выход");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddObject("Добавление линии", "X1, Y1, X2, Y2", 0, 4);
                        break;
                    case "2":
                        AddObject("Добавление прямоугольника", "X, Y, ширина, высота", 1, 4);
                        break;
                    case "3":
                        AddObject("Добавление окружности", "X, Y, радиус", 2, 3);
                        break;
                    case "4":
                        AddObject("Добавление круга", "X, Y, радиус", 3, 3);
                        break;
                    case "5":
                        AddObject("Добавление кольца", "X, Y, внутренний и внешний радиусы", 4, 4);
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}
