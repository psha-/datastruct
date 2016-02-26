using System;

namespace StacksQueues
{
	public class LinkedStack<T>
	{
		private Node<T> firstNode;
		public int Count { get; private set; }

		public void Push(T element)
		{
			firstNode = new Node<T> (element, firstNode);
			Count++;
		}

		public T Pop()
		{
			if (0 == Count) {
				throw new InvalidOperationException ("Stack empty");
			}

			Count--;

			var ret = firstNode;
			firstNode = firstNode.NextNode;

			return ret.Value;
		}

		public T[] ToArray()
		{
			var arr = new T[Count];
			var node = firstNode;
			for( var i=0; i < Count; i++ ) {
				arr [i] = node.Value;
				node = node.NextNode;
			}
			return arr;
		}

		private class Node<T>
		{
			public T Value{ get; private set;}
			public Node<T> NextNode;
			public Node(T v, Node<T> n = null) {
				Value = v;
				NextNode = n;
			}
		}
	}
}

