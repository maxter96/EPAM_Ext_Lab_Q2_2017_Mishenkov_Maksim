namespace Task03
{
    using System;

    public class Figure : IDrawable
    {
        public Figure()
        {
        }

        public Figure(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        void IDrawable.ShowInfo()
        {
            Console.WriteLine(this);
        }
    }
}
