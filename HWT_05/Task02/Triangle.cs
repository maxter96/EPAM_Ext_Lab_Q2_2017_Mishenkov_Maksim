namespace Task02
{
	using System;

	public class Triangle
	{
		private double sideA;
		private double sideB;
		private double sideC;
		private double area;
		private double perimeter;

		public Triangle() : this(1, 1, 1) { }

		public Triangle(double a, double b, double c)
		{
			if (a == 0 || b == 0 || c == 0 || a + b <= c || a + c <= b || b + c <= a)
			{
				Console.WriteLine("Некорректные значения длин! Установлены значения по умолчанию: 1");
				sideA = sideB = sideC = 1;
			}
			else
			{
				sideA = a;
				sideB = b;
				sideC = c;
			}

			perimeter = sideA + sideB + sideC;
			double half = perimeter / 2;
			area = Math.Sqrt(half * (half - sideA) * (half - sideB) * (half - sideC));
		}

		public double A
		{
			get { return sideA; }
		}

		public double B
		{
			get { return sideB; }
		}

		public double C
		{
			get { return sideC; }
		}

		public double Perimeter
		{
			get { return perimeter; }
		}

		public double Area
		{
			get { return area; }
		}
	}
}
