using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderedSet
{
	public class Node<T>: IEnumerable<T>
	{
		public T Value{ get; private set;}
		public Node<T> Left{ get; set; }
		public Node<T> Right{ get; set; }
		public bool Visited;

		public Node (T val)
		{
			Value = val;
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (null != Left) {
				var childEnumerator = Left.GetEnumerator ();
				while( childEnumerator.MoveNext() )
					yield return childEnumerator.Current;
			}
			yield return Value;
			if (null != Right) {
				var childEnumerator = Right.GetEnumerator ();
				while( childEnumerator.MoveNext() )
					yield return childEnumerator.Current;
			}

		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator ();
		}


	}
}

