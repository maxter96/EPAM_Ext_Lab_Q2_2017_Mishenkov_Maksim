namespace Task03
{
    using System;

    public class Ring : Figure, IFillable
    {
        private const double DefaultInnerRadius = 1;
        private const double DefaultOuterRadius = 1;
        private Round innerRound;
        private Round outerRound;

        public Ring()
        {
            innerRound = new Round();
            outerRound = new Round();
        }

        public Ring(double x, double y, double innerRadius, double outerRadius)
        {
            if (innerRadius <= 0)
            {
                innerRadius = DefaultInnerRadius;
                Console.WriteLine("Внутренний радиус должен быть больше нуля!");
                Console.WriteLine("Установлено значение по умолчанию: {0:f3}", innerRadius);
            }

            if (outerRadius <= 0)
            {
                outerRadius = DefaultOuterRadius;
                Console.WriteLine("Внешний радиус должен быть больше нуля!");
                Console.WriteLine("Установлено значение по умолчанию: {0:f3}", outerRadius);
            }

            if (outerRadius < innerRadius)
            {
                innerRadius = DefaultInnerRadius;
                outerRadius = DefaultOuterRadius;
                Console.WriteLine("Внешний радиус не может быть меньше внутреннего!");
                Console.WriteLine("Установлены значения по умолчанию: {0:f3}; {1:f3}", innerRadius, outerRadius);
            }

            innerRound = new Round(x, y, innerRadius);
            outerRound = new Round(x, y, outerRadius);
        }

        public new double X
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

        public new double Y
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

        public double GetArea()
        {
            return outerRound.GetArea() - innerRound.GetArea();
        }

        public override string ToString()
        {
            return string.Format(
                "Кольцо. Координаты: ({0:f3};{1:f3}); Радиусы: {2:f3} {3:f3}; Площадь: {4:f3}",
                X,
                Y,
                innerRound.Radius,
                outerRound.Radius,
                GetArea());
        }
    }
}
