namespace Task03
{
    using System;

    public class Circle : Figure
    {
        private const double DefaultRadius = 1;
        private double radius;

        public Circle()
        {
            Radius = DefaultRadius;
        }

        public Circle(double x, double y, double radius) : base(x,y)
        {
            Radius = radius;
        }

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
                    value = DefaultRadius;
                    Console.WriteLine("Радиус должен быть больше нуля!");
                    Console.WriteLine("Установлено значение по умолчанию: {0:f3}", value);
                }

                radius = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Окружность. Координаты: ({0:f3};{1:f3}); Радиус: {2:f3};",
                X,
                Y,
                Radius);
        }
    }
}
