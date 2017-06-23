namespace Task03
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public class DynamicArray<T> : IEnumerable<T>, IEnumerator<T>
		where T : IComparable<T>
	{
		private const int DefaultSize = 8;
		private const int DefaultEnumIndex = -1;
		private const int DefaultElementIndex = -1;
		private T[] array;
		private int currentIndex;
		private int enumIndex;

		public DynamicArray()
			: this(DefaultSize) { }

		public DynamicArray(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentException("Размер массива должен быть больше нуля!");
			}

			array = new T[count];
			currentIndex = 0;
			Reset();
		}

		public DynamicArray(IEnumerable<T> collection)
		{
			array = collection.ToArray();
			currentIndex = array.Length;
			Reset();
		}

		public void Add(T element)
		{
			if (Length + 1 >= Capacity)
			{
				IncreaseSize(1);//todo pn лучше увеличивать на какое-то большее число (например, на 20 элементов или на 100), ну, и, понятно, что выносить это число в константу. Иначе просто смысл динамического массива теряется.
			}

			array[currentIndex] = element;
			currentIndex++;
		}

		public void AddRange(IEnumerable<T> range)
		{
			int rangeCount = range.Count();

			if (currentIndex + rangeCount >= Capacity)
			{
				IncreaseSize(rangeCount);
			}

			foreach (T element in range)
			{
				array[currentIndex] = element;
				currentIndex++;
			}
		}

		public bool Remove(T element)
		{
			int index = IndexOf(element);

			if (index == DefaultElementIndex)
			{
				return false;
			}

			array[index] = default(T);
			int i = 0;

			for (i = index; i < Length; i++)
			{
				if (i == Length - 1)
				{
					array[i] = default(T);
				}
				else
				{
					array[i] = array[i + 1];
				}
			}

			array[i] = default(T);
			currentIndex--;
			return true;
		}

		public bool Insert(int position, T element)
		{
			if (position < 0 || position >= Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (Length + 1 > Capacity)
			{
				IncreaseSize(1);
			}

			for (int i = Length; i > position; i--)
			{
				array[i] = array[i - 1];
			}

			array[position] = element;
			currentIndex++;
			return true;
		}

		public int Length
		{
			get { return currentIndex; }
		}

		public int Capacity
		{
			get { return array.Length; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		public void Dispose() { }

		public bool MoveNext()
		{
			enumIndex++;

			if (enumIndex >= currentIndex)
			{
				Reset();
				return false;
			}

			return true;
		}

		public void Reset()
		{
			enumIndex = DefaultEnumIndex;
		}

		public T Current
		{
			get { return array[enumIndex]; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
				{
					throw new ArgumentOutOfRangeException();
				}

				return array[index];
			}

			set
			{
				if (index < 0 || index >= Length)
				{
					throw new ArgumentOutOfRangeException();
				}

				array[index] = value;
			}
		}

		private void IncreaseSize(int count)
		{
			double needSize = Math.Ceiling((double)(Capacity + count) / Capacity);
			int n = (int)Math.Ceiling(Math.Log(needSize, 2));
			int increasingLevel = (int)Math.Pow(n, 2);
			increasingLevel = Math.Max(2, increasingLevel);
			var tempArray = new T[Capacity * increasingLevel];
			array.CopyTo(tempArray, 0);
			array = tempArray;
		}

		private int IndexOf(T element)
		{
			for (int i = 0; i < Length; i++)
			{
				if (array[i].CompareTo(element) == 0)
				{
					return i;
				}
			}

			return DefaultElementIndex;
		}
	}
}
