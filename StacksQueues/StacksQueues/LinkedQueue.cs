using System;

namespace StacksQueues
{
	public class LinkedQueue<T>
	{
		public int Count { get; private set; }
		private QueueNode<T> Head;
		private QueueNode<T> Tail;

		public void Enqueue(T element)
		{
			var node = new QueueNode<T> (element);
			if (0 == Count) {
				Head = Tail = node;
			} else {
				Tail.NextNode = node;
				node.PrevNode = Tail;
				Tail = node;
			}

			Count++;
		}

		public T Dequeue()
		{
			if (0 == Count) {
				throw new InvalidOperationException ("Queue is empty");
			}
			var ret = Head;
			if (1 == Count) {
				Head = Tail = null;
			} else {
				Head = Head.NextNode;
				Head.PrevNode = null;
			}
			Count--;

			return ret.Value;
		}

		public T[] ToArray()
		{
			var arr = new T[Count];
			var node = Head;
			for( var i=0; i< Count; i++ ) {
				arr [i] = node.Value;
				node = node.NextNode;
			}
			return arr;
		}

		private class QueueNode<T>
		{
			public QueueNode(T value)
			{
				Value = value;
			}
			public T Value { get; private set; }
			public QueueNode<T> NextNode { get; set; }
			public QueueNode<T> PrevNode { get; set; }
		}
	}
}

