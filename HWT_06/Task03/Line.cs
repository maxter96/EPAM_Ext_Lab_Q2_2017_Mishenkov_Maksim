namespace Task03
{
    public class Line : Figure
    {
        public Line(double x1, double y1, double x2, double y2) : base(x1, y1)
        {
            X2 = x2;
            Y2 = y2;
        }

        public Line()
        {
        }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public override string ToString()
        {
            return string.Format("Линия. Координаты: ({0:f3};{1:f3}) ({2:f3};{3:f3})", X, Y, X2, Y2);
        }
    }
}
