namespace Task02
{
    using System;
    using System.Text;

    public class Program
    {
        private static void ShowRingInfo(Ring ring)
        {
            Console.WriteLine(ring);
            Console.WriteLine("Площадь: {0:f4}", ring.Area);
            Console.WriteLine("Суммарная длина окружностей: {0:f4}\n", ring.TotalLength);
        }

        private static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("Кольцо с конструктором по умолчанию");
            Ring defaultRing = new Ring();
            ShowRingInfo(defaultRing);

            double x = 0.5;
            double y = 0.7;
            double innerRadius = 3.6;
            double outerRadius = 4.8;
            Console.WriteLine("Кольцо с заданными параметрами");
            Ring goodRing = new Ring(x, y, innerRadius, outerRadius);
            ShowRingInfo(goodRing);

            innerRadius = 4;
            outerRadius = -2;
            Console.WriteLine("Кольцо с некорректными параметрами");
            Ring badRing = new Ring(x, y, innerRadius, outerRadius);
            ShowRingInfo(badRing);

            Console.ReadKey();
        }
    }
}
