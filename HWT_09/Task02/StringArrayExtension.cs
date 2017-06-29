namespace Task02
{
	public static class StringArrayExtension
	{
		public static bool IsPositiveInteger(this string input)
		{
			bool isPositive = false;

			if (string.IsNullOrEmpty(input))
			{
				return false;
			}

			for (int i = 0; i < input.Length; i++)
			{
				if (!char.IsDigit(input[i]))
				{
					return false;
				}

				var value = char.GetNumericValue(input[i]);

				if (value > 0)
				{
					isPositive = true;
				}
			}

			return isPositive;
		}
	}
}
