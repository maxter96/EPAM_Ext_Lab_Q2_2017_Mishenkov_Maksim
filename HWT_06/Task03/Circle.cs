namespace Task03
{
    using System;

    public class Circle : IDrawable
    {
        private const double DefaultRadius = 1;
        private double radius;

        public Circle()
        {
            Radius = DefaultRadius;
        }

        public Circle(double x, double y, double radius)
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

        public void ShowInfo()//todo pn у тебя этот метод повторяется 3 раза с одинаковым телом. Мб стоит вынести его в базовый класс для всех фигур?
        {
            Console.WriteLine(this);
        }
    }
}
