namespace Task03
{
    using System;

    public class Rectangle : Figure, IFillable
    {
        private const double DefaultWidth = 1;
        private const double DefaultHeight = 1;
        private double width;
        private double height;

        public Rectangle(double x, double y, double width, double height) : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public Rectangle()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;
        }

        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value <= 0)
                {
                    value = DefaultWidth;
                    Console.WriteLine("Ширина должна быть больше нуля!");
                    Console.WriteLine("Установлено значение по умолчанию: {0};", value);
                }

                width = value;
            }
        }

        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                if (value <= 0)
                {
                    value = DefaultHeight;
                    Console.WriteLine("Высота должна быть больше нуля!");
                    Console.WriteLine("Установлено значение по умолчанию: {0};", value);
                }

                height = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Прямоугольник. Координаты: ({0:f3};{1:f3}); Ширина: {2:f3}; Высота: {3:f3}; Площадь: {4:f3};",
                X,
                Y,
                Width,
                Height,
                GetArea());
        }

        public double GetArea()
        {
            return Width * Height;
        }
    }
}
