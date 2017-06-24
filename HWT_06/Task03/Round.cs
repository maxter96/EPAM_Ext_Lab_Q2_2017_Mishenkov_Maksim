namespace Task03
{
    using System;

    public class Round : Circle, IFillable
    {
        public Round(double x, double y, double radius) : base(x, y, radius) { }

        public Round() : base() { }

        public double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override string ToString()
        {
            return string.Format(
                "Круг. Координаты: ({0:f3};{1:f3}); Радиус: {2:f3}; Площадь: {3:f3}",
                X,
                Y,
                Radius,
                GetArea());
        }
	}
}
