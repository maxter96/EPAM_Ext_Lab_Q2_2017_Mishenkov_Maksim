namespace Task02
{
	using System;

	public class Round
    {
        private const double DefaultX = 0;
        private const double DefaultY = 0;
        private const double DefaultRadius = 1;
        private double radius;

		public Round()
		{
            X = DefaultX;
            Y = DefaultY;
			radius = DefaultRadius;
		}

        public Round(double radius)
        {
            X = DefaultX;
            Y = DefaultY;
            Radius = radius;
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
					radius = DefaultRadius;
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
