namespace Task02
{
	public static class StringArrayExtension
	{
		public static bool IsPositiveInteger(this string input)//todo pn не нужно переделывать
		{
			bool isPositive = false;

			if (string.IsNullOrEmpty(input))
			{
				return false;//todo pn не нужно делать столько выходов из метода, просто описывай нужную тебе ситуацию, а в случае остальный проходи мимо
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
					isPositive = true;//todo pn чтобы осталось только это возвращение
				}
			}

			return isPositive;//todo pn и это
		}
	}
}
