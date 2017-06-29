namespace Task01
{
	public static class ArrayExtension
	{
		public static int GetSum(this int[] array)
		{
			int sum = 0;

			if (array == null)
			{
				return sum;
			}

			foreach (int element in array)
			{
				sum += element;
			}

			return sum;
		}
	}
}
