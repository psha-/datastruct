using System;

namespace StacksQueues
{
	public class ArrayStack<T>
	{
		private T[] elements;
		public uint Count { get; private set; }
		private const uint InitialCapacity = 16;

		public ArrayStack(uint capacity = InitialCapacity) {
			elements = new T[InitialCapacity];
		}

		public void Push(T element) {
			if (elements.Length == Count) {
				Grow ();
			}
			elements [Count] = element;
			Count++;
		}

		public T Pop() {
			if (Count == 0) {
				throw new InvalidOperationException ("The stack is empty");
			}
			Count--;
			T ret = elements [Count];
			elements [Count] = default(T);
			return ret;

		}

		public T[] ToArray() {
			var arr = new T[Count];
			for( var i=0; i <Count ; i++ ) {
				arr [i] = elements[Count-1-i];
			}
			return arr;
		}

		private void Grow() {
			var newElements = new T[elements.Length * 2];
			Array.Copy( elements, newElements, Count);
			elements = newElements;
		}
	}
}

