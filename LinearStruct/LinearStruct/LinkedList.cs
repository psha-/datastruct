using System;
using System.Collections;
using System.Collections.Generic;

namespace LinearStruct
{
	public class ListNode<T>
	{
		public ListNode(T v)
		{
			val = v;
		}
		public T val{ get; private set; }
		public ListNode<T> next;
	}

	public class LinkedList<T>: IEnumerable<T> where T : IComparable
	{

		public ListNode<T> Head { get; private set; }
		public ListNode<T> Tail { get; private set; }
		int Count;

		public LinkedList ()
		{
			Count = 0;
		}

		public void Add(T item) {
			var node = new ListNode<T> (item);
			if (0 == Count) {
				Head = Tail = node;
			} else {
				Tail.next = node;
				Tail = node;
			}
			Count++;
		}

		public void Remove(int index)
		{
			if (index < 0 || index > Count - 1) {
				throw new IndexOutOfRangeException ();
			}
			if (0 == index) {
				Head = Head.next;
			} else {
				var node = Head;
				for (var i = 0; i<index-1; i++) {
					node = node.next;
				}
				node.next = node.next.next;
				if (Count - 1 == index) {
					Tail = node;
				}
			}
			Count--;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator ();
		}

		public IEnumerator<T> GetEnumerator()
		{
			var node = Head;
			for (var i=0; i<Count; i++) {
				yield return node.val;
				node = node.next;
			}
		}

		public int FirstIndexOf(T val) {
			var i = 0;
			foreach (var item in this) {
				if (0 == item.CompareTo(val)) {
					return i;
				}
				i++;
			}
			return -1;
		}

		public int LastIndexOf(T val) {
			var i = 0;
			var index = -1;
			foreach (var item in this) {
				if (0 == item.CompareTo(val)) {
					index = i;
				}
				i++;
			}
			return index;
		}
	}
}

