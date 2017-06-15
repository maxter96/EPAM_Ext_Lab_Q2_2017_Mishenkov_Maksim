namespace Task02
{
    using System;

    public class Ring
    {
        private const double DefaultInnerRadius = 1;
        private const double DefaultOuterRadius = 2;
        private Round innerRound;
        private Round outerRound;

        public Ring()
        {
            innerRound = new Round(DefaultInnerRadius);
            outerRound = new Round(DefaultOuterRadius);
        }

        public Ring(double x, double y, double innerRadius, double outerRadius)
        {
            if (innerRadius <= 0)
            {
                innerRadius = DefaultInnerRadius;
                Console.WriteLine("Внутренний радиус не может быть отрицательным!");
                Console.WriteLine("Установлено значение по умолчанию: {0:f4}", innerRadius);
            }

            if (outerRadius <= 0)
            {
                outerRadius = DefaultOuterRadius;
                Console.WriteLine("Внешний радиус не может быть отрицательным!");
                Console.WriteLine("Установлено значение по умолчанию: {0:f4}", outerRadius);
            }

            if (outerRadius < innerRadius)
            {
                innerRadius = DefaultInnerRadius;
                outerRadius = DefaultOuterRadius;
                Console.WriteLine("Внешний радиус не может быть меньше внутреннего!");
                Console.WriteLine("Установлены значения по умолчанию: {0:f4}; {1:f4}", innerRadius, outerRadius);
            }

            innerRound = new Round(x, y, innerRadius);
            outerRound = new Round(x, y, outerRadius);
        }

        public double X
        {
            get
            {
                return innerRound.X;
            }

            set
            {
                innerRound.X = value;
                outerRound.X = value;
            }
        }

        public double Y
        {
            get
            {
                return innerRound.Y;
            }

            set
            {
                innerRound.Y = value;
                outerRound.Y = value;
            }
        }

        public double Area
        {
            get { return outerRound.Area - innerRound.Area; }
        }

        public double TotalLength
        {
            get { return outerRound.Length + innerRound.Length; }
        }

        public override string ToString()
        {
            return string.Format(
                "Кольцо с координатами ({0:f4};{1:f4}). Радиусы: {2:f4}; {3:f4};",
                X,
                Y,
                innerRound.Radius,
                outerRound.Radius);
        }
    }
}
