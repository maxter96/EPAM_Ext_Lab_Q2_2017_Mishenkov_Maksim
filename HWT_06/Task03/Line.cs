namespace Task03
{
    using System;

    public class Line : IDrawable
    {
        public Line(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Line()
        {
        }

        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public override string ToString()
        {
            return string.Format("Линия. Координаты: ({0:f3};{1:f3}) ({2:f3};{3:f3})", X1, Y1, X2, Y2);
        }

        public void ShowInfo()
        {
            Console.WriteLine(this);
        }
    }
}
