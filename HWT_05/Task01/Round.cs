namespace Task01
{
	using System;

	public class Round
	{
		private double radius;

		public Round()
		{
			X = 0;//todo pn значения по умолчанию лучше в константы
			Y = 0;
			radius = 1;
		}

		public Round(double x, double y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}

		public double X { get; set; }

		public double Y { get; set; }

		public double Radius
		{
			get
			{
				return radius;
			}

			set
			{
				if (value <= 0)
				{
					radius = 1;
					Console.WriteLine("Введено некорректное значение радиуса!");
					Console.WriteLine("Установлено значение по умолчанию: 1");
				}
				else
				{
					radius = value;
				}
			}
		}

		public double Length
		{
			get { return 2 * Math.PI * radius; }
		}

		public double Area
		{
			get { return Math.PI * Math.Pow(radius, 2); }
		}
	}
}
