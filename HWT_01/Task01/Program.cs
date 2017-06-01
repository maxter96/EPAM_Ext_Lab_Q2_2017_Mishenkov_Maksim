namespace Task01
{
	using System;
	using System.Threading;
	using System.Text;

	struct Point
	{
		public double X;
		public double Y;

		public Point(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
	}

	class Program
	{
		private static bool IsInCircle(Point center, double radius, Point point, bool withBounds)
		{
			double distance = Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2);

			if (withBounds)
			{
				return distance <= Math.Pow(radius, 2);
			}
			else
			{
				return distance < Math.Pow(radius, 2);
			}
		}

		private static bool IsInRectangle(Point upperLeft, Point bottomRight, Point point)
		{
			return point.X >= upperLeft.X
				&& point.X <= bottomRight.X
				&& point.Y >= bottomRight.Y
				&& point.Y <= upperLeft.Y;
		}

		private static double AreaOfTriangle(Point p1, Point p2, Point p3)
		{
			return Math.Abs(
				(p2.X * p3.Y) - (p3.X * p2.Y)
				- (p1.X * p3.Y) + (p3.X * p1.Y)
				+ (p1.X * p2.Y) - (p2.X * p1.Y));
		}

		private static bool IsInTriangle(Point vert1, Point vert2, Point vert3, Point point)
		{
			double sumOfAreas = AreaOfTriangle(vert1, vert2, point);
			sumOfAreas += AreaOfTriangle(vert2, vert3, point);
			sumOfAreas += AreaOfTriangle(vert3, vert1, point);
			double areaOfTriangle = AreaOfTriangle(vert1, vert2, vert3);
			return sumOfAreas == areaOfTriangle;
		}

		private static bool IsInFigureG(Point point)
		{
			Point absPoint = new Point(Math.Abs(point.X), Math.Abs(point.Y));
			Point p1 = new Point(0, 0);
			Point p2 = new Point(0, 1);
			Point p3 = new Point(1, 0);
			return IsInTriangle(p1, p2, p3, absPoint);
		}

		private static bool IsInFigureD(Point point)
		{
			Point absPoint = new Point(Math.Abs(point.X), Math.Abs(point.Y));
			Point p1 = new Point(0, 0);
			Point p2 = new Point(0, 1);
			Point p3 = new Point(1, 0);
			return IsInTriangle(p1, p2, p3, absPoint);
		}

		private static bool IsInFigureE(Point point)
		{
			if (point.X > 0)
			{
				return IsInCircle(new Point(0, 0), 1, point, true);
			}

			Point p1 = new Point(-2, 0);
			Point p2 = new Point(0, 1);
			Point p3 = new Point(0, -1);
			return IsInTriangle(p1, p2, p3, point);
		}

		private static bool IsInFigureJ(Point point)
		{
			double firstHeight = 2;
			double secondHeight = 3;
			double x = secondHeight / firstHeight;
			Point p1 = new Point(0, 2);
			Point p2 = new Point(x, -1);
			Point p3 = new Point(-x, -1);
			return IsInTriangle(p1, p2, p3, point);
		}

		private static bool IsInFigureZ(Point point)
		{
			Point center = new Point(0, 0);
			Point p1 = new Point(-1, 0);
			Point p2 = new Point(-1, 1);
			Point p3 = new Point(1, 0);
			Point p4 = new Point(1, 1);
			Point p5 = new Point(-1, 0);
			Point p6 = new Point(-1, -2);
			return IsInTriangle(p1, p2, center, point)
				|| IsInTriangle(p3, p4, center, point)
				|| IsInRectangle(p5, p6, point);
		}

		private static bool IsInFigureI(Point point)
		{
			Point p1 = new Point(-2, -1);
			Point p2 = new Point(0, 0);
			Point p3 = new Point(-1, 1);
			Point p4 = new Point(1, 0);
			return IsInTriangle(p1, p3, p2, point)
				|| IsInTriangle(p1, p2, p4, point);
		}

		private static bool IsInFigureK(Point point)
		{
			Point p1 = new Point(-1, 1);
			Point p2 = new Point(1, 1);
			Point p3 = new Point(0, 0);
			return point.Y >= 1 || IsInTriangle(p1, p2, p3, point);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Point point;
			char figure;
			bool result = true;
			while (true)
			{
				Console.Clear();
				Console.Write("Введите координаты точки через пробел: ");
				string input = Console.ReadLine();
				string[] coords = input.Split(' ');

				if (coords.Length == 2)
				{
					double x = 0.0;
					double y = 0.0;

					if (double.TryParse(coords[0], out x) && double.TryParse(coords[1], out y))
					{
						point.X = x;
						point.Y = y;
						break;
					}
				}

				Console.Write("Некорректный ввод точки. Попробуйте снова!");
				Thread.Sleep(1500);
			}

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Координаты точки: ({0} ; {1})", point.X, point.Y);
				Console.Write("Введите номер фигуры: ");
				string input = Console.ReadLine();

				if (!char.TryParse(input, out figure))
				{
					Console.WriteLine("Фигуры с таким именем не существует! Попробуйте снова!");
					Thread.Sleep(1500);
					continue;
				}

				switch (figure)
				{
					case 'а':
						result = IsInCircle(new Point(0, 0), 1, point, true);
						break;
					case 'б':
						var center = new Point(0, 0);
						result = IsInCircle(center, 1, point, true)
							&& !IsInCircle(center, 0.5, point, false);
						break;
					case 'в':
						result = IsInRectangle(new Point(-1, 1), new Point(1, -1), point);
						break;
					case 'г':
						result = IsInFigureG(point);
						break;
					case 'д':
						result = IsInFigureD(point);
						break;
					case 'е':
						result = IsInFigureE(point);
						break;
					case 'ж':
						result = IsInFigureJ(point);
						break;
					case 'з':
						result = IsInFigureZ(point);
						break;
					case 'и':
						result = IsInFigureI(point);
						break;
					case 'к':
						result = IsInFigureK(point);
						break;
					default:
						Console.WriteLine("Фигуры с таким именем не существует! Попробуйте снова!");
						Thread.Sleep(1500);
						continue;
				}

				break;
			}

			if (result)
			{
				Console.WriteLine("Точка с координатами ({0} ; {1}) принадлежит фигуре '{2}'", point.X, point.Y, figure);
			}
			else
			{
				Console.WriteLine("Точка с координатами ({0} ; {1}) не принадлежит фигуре '{2}'", point.X, point.Y, figure);
			}

			Console.ReadKey();
		}
	}
}
