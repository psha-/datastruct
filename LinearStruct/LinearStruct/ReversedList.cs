using System;
using System.Collections;
using System.Collections.Generic;

namespace LinearStruct
{
	public class ReversedList<T>: IEnumerable<T>
	{
		private List<T> arr;
		public int Count { get; private set;}
		public int Capacity { get; private set;}

		public ReversedList()
		{
			arr = new List<T> ();
			Capacity = 0;
			Count = 0;
		}

		public void Add(T item)
		{
			if (Count == Capacity) {
				Capacity *= 2;
				arr.Capacity = Capacity;
			}

			arr.Add (item);
			Count++;
		}

		public void Remove(int id)
		{
			arr.RemoveAt (Count - 1 - id);
			Count--;
		}

		public T this[int id]
		{
			get
			{
				return arr[Count - 1 - id];
			}
			set
			{
				arr[Count - 1 - id] = value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator ();
		}
		public IEnumerator<T> GetEnumerator()
		{

			for (var i=Count-1; i>=0; i--) {
				yield return arr[i];
			}
		}
	}
}

