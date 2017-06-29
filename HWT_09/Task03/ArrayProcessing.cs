namespace Task03
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class ArrayProcessing
	{
		public static int[] FindAllPositive(int[] array)
		{
			if (array == null)
			{
				return new int[0];
			}

			List<int> result = new List<int>();

			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0)
				{
					result.Add(array[i]);
				}
			}

			return result.ToArray();
		}

		public static int[] FindAllElements(int[] array, Predicate<int> predicate)
		{
			if (array == null)
			{
				return new int[0];
			}

			List<int> result = new List<int>();

			for (int i = 0; i < array.Length; i++)
			{
				if (predicate(array[i]))
				{
					result.Add(array[i]);
				}
			}

			return result.ToArray();
		}

		public static int[] FindAllPositiveByLinq(int[] array)
		{
			return array.Where(x => x > 0).ToArray();
		}
	}
}
