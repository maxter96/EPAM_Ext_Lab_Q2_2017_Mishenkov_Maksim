namespace Task03
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;

	public class Program
	{
		private static Random random;
		private static Stopwatch stopWatch;
		private static int maxTestCount = 41;
		private static int arraySize = 1000;

		private static long GetMedian(long[] time)
		{
			if (!time.Any())
			{
				return -1;
			}

			Array.Sort(time);
			return time[time.Length / 2];
		}

		private static bool CheckPositive(int value)
		{
			return value > 0;
		}

		private static long CheckTime(int[] values, Func<int[], int[]> getValues)
		{
			long[] time = new long[maxTestCount];
			int[] result;

			stopWatch.Restart();
			result = getValues(values);
			stopWatch.Stop();

			for (int i = 0; i < maxTestCount; i++)
			{
				stopWatch.Restart();
				result = getValues(values);
				stopWatch.Stop();
				time[i] = stopWatch.ElapsedTicks;
			}

			return GetMedian(time);
		}

		private static long CheckTime(int[] values, Func<int[], Predicate<int>, int[]> getValues, Predicate<int> predicate)
		{
			long[] time = new long[maxTestCount];
			int[] result;

			stopWatch.Restart();
			result = getValues(values, predicate);
			stopWatch.Stop();

			for (int i = 0; i < maxTestCount; i++)
			{
				stopWatch.Restart();
				result = getValues(values, predicate);
				stopWatch.Stop();
				time[i] = stopWatch.ElapsedTicks;
			}

			return GetMedian(time);
		}

		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			random = new Random();
			stopWatch = new Stopwatch();
			maxTestCount = 41;
			arraySize = 1000;
			int[] values = new int[arraySize];

			for (int i = 0; i < arraySize; i++)
			{
				values[i] = random.Next(-100, 100);
			}

			Predicate<int> dlg = delegate(int value)
			{
				return value > 0;
			};

			long res1 = CheckTime(values, ArrayProcessing.FindAllPositive);
			long res2 = CheckTime(values, ArrayProcessing.FindAllElements, CheckPositive);
			long res3 = CheckTime(values, ArrayProcessing.FindAllElements, dlg);
			long res4 = CheckTime(values, ArrayProcessing.FindAllElements, x => x > 0);
			long res5 = CheckTime(values, ArrayProcessing.FindAllPositiveByLinq);

			Console.WriteLine("Результат работы (в тиках) на массиве из {0} элементов\n", arraySize);
			Console.WriteLine("{0} {1}", "Поиск напрямую:".PadRight(30), res1);
			Console.WriteLine("{0} {1}", "Передача делегата:".PadRight(30), res2);
			Console.WriteLine("{0} {1}", "Передача анонимного метода:".PadRight(30), res3);
			Console.WriteLine("{0} {1}", "Передача лямбда-выражения:".PadRight(30), res4);
			Console.WriteLine("{0} {1}", "LINQ запрос:".PadRight(30), res5);

			Console.ReadKey();
		}
	}
}
