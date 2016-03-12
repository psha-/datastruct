using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderedSet
{

	public class OrderedSet<T>:IEnumerable<T> where T : IComparable<T>
	{
		public int Count{get; private set;}
		public Node<T> Root{get; private set;}

		public OrderedSet ()
		{
		}


		public void Add(T element)
		{	
			if (null == Root) {
				Root = new Node<T> (element);
				Count++;
				return;
			}
			if( Contains(element)) {
				return;
			}
			AddChild (element, Root);
		}

		private void AddChild (T element, Node<T> node)
		{
			if (element.CompareTo (node.Value) < 0) {
				AddLeft (element, node);
			} else {
				AddRight (element, node);
			}
		}

		private void AddLeft( T element, Node<T> node ) {
			if (null == node.Left) {
				node.Left = new Node<T> (element);
				Count++;
			} else {
				AddChild (element, node.Left);
			}
		}

		private void AddRight( T element, Node<T> node )
		{
			if (null == node.Right) {
				node.Right = new Node<T> (element);
				Count++;
			} else {
				AddChild (element, node.Right);
			}
		}

		public bool Contains(T element)
		{
			if (0 == Count) {
				return false;
			}
			var node = Root;
			while (null != node) {
				if (0 == element.CompareTo (node.Value)) {
					return true;
				}
				else if (element.CompareTo (node.Value) < 0) {
					node = node.Left;
				} else {
					node = node.Right;
				}
			}
			return false;
		}

		public IEnumerator<T> GetEnumerator()
		{
			var setEnumerator = Root.GetEnumerator ();
			while (setEnumerator.MoveNext()) {
				yield return setEnumerator.Current;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator ();
		}


		private void GetOrderedChildren(Node<T> node, ref Node<T> smaller, ref Node<T> bigger)
		{
			if (null == node.Left) {
				bigger = node.Right;
			}
			else if (null == node.Right) {
				bigger = node.Left;
			}
			else if (node.Left.Value.CompareTo (node.Right.Value) < 0) {
				smaller = node.Left;
				bigger = node.Right;
			} else {
				smaller = node.Right;
				bigger = node.Left;
			}
		}

		private void ReconnectSubtree(Node<T> subtree)
		{
			var node = Root;
			while (null != node) {
				if (subtree.Value.CompareTo (node.Value) < 0) {
					if (null == node.Left) {
						node.Left = subtree;
						return;
					}
					node = node.Left;
				} else {
					if (null == node.Right) {
						node.Right = subtree;
						return;
					}
					node = node.Right;
				}
			}
		}


		private void RemoveNode(Node<T> node) {

		}

		public void Remove (T element)
		{
			if (0 == Count) {
				throw new InvalidOperationException ("Set is empty.");
			}
			if (0 == element.CompareTo (Root.Value)) {
				Node<T> smaller = null;
				Node<T> bigger = null;
				GetOrderedChildren (Root, ref smaller, ref bigger);
				Root = bigger;
				if (null != smaller) {
					ReconnectSubtree (smaller);
				}
				Count--;
				return;
			}

			var node = Root;
			while (null != node) {
				// The following is to avoid keeping Parent node.
				if (null != node.Left && 0 == element.CompareTo (node.Left.Value)) {
					Node<T> smaller = null;
					Node<T> bigger = null;
					GetOrderedChildren (node.Left, ref smaller, ref bigger);
					node.Left = bigger;
					if (null != smaller) {
						ReconnectSubtree (smaller);
					}
					Count--;
					return;
				}
				if (null != node.Right && 0 == element.CompareTo (node.Right.Value)) {
					Node<T> smaller = null;
					Node<T> bigger = null;
					GetOrderedChildren (node.Right, ref smaller, ref bigger);
					node.Right = bigger;
					if (null != smaller) {
						ReconnectSubtree (smaller);
					}
					Count--;
					return;
				}

				if (element.CompareTo (node.Value) < 0) {
					node = node.Left;
				} else {
					node = node.Right;
				}
			}


		}

	}
}

