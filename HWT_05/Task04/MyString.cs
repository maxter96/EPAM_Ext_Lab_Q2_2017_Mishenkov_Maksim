namespace Task04
{
	using System;
	using System.Linq;

	public class MyString
	{
		private char[] str;

		public MyString()
		{
			str = new char[0];
		}

		public MyString(string value)
		{
			str = value.ToCharArray();
		}

		public MyString(char[] arr)
		{
			str = arr.ToArray();
		}

		public int Length
		{
			get { return str.Length; }
		}

		public override string ToString()
		{
			return new string(str);
		}

		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
				{
					throw new IndexOutOfRangeException();
				}

				return str[index];
			}

			set
			{
				str[index] = value;
			}
		}

		public MyString SubString(int startIndex, int length)
		{
			if (startIndex < 0 || length < 0 || startIndex + length >= Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			var arr = str.Skip(startIndex)
				.Take(length)
				.ToArray();
			return new MyString(arr);
		}

		public int IndexOf(char symbol)
		{
			return Array.IndexOf(str, symbol);
		}

		public int LastIndexOf(char symbol)
		{
			return Array.LastIndexOf(str, symbol);
		}

		public MyString Insert(int index, MyString value)
		{
			if (index < 0 || index > Length)
			{
				throw new IndexOutOfRangeException();
			}

			if (value == null)
			{
				throw new ArgumentNullException();
			}

			str = str.Take(index)
				.Concat(value.str)
				.Concat(str.Skip(index))
				.ToArray();
			return new MyString(str);
		}

		public MyString Replace(char oldChar, char newChar)
		{
			for (int i = 0; i < Length; i++)
			{
				if (str[i] == oldChar)
				{
					str[i] = newChar;
				}
			}

			return new MyString(str);
		}

		public MyString Remove(int startIndex, int count)
		{
			if (startIndex < 0 || count < 0 || startIndex + count > Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			str = str.Take(startIndex)
				.Concat(str.Skip(startIndex + count))
				.ToArray();
			return new MyString(str);
		}

		public MyString Trim()
		{
			int i = 0;
			while (i < Length)
			{
				if (!char.IsWhiteSpace(str[i]))
				{
					break;
				}

				i++;
			}

			Remove(0, i);

			i = Length - 1;
			while (i >= 0)
			{
				if (!char.IsWhiteSpace(str[i]))
				{
					break;
				}

				i--;
			}

			Remove(i + 1, Length - i - 1);
			return new MyString(str);
		}

		public bool Contains(char symbol)
		{
			return str.Contains(symbol);
		}

		public MyString ToLower()
		{
			str = str.Select(x => char.ToLower(x))
				.ToArray();
			return new MyString(str);
		}

		public MyString ToUpper()
		{
			str = str.Select(x => char.ToUpper(x))
				.ToArray();
			return new MyString(str);
		}
	}
}
