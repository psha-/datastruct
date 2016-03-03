using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeTraversal
{
	public class Node<T>
	{
		public T Value { get; private set; }
		public Node<T> Parent;
		public Node(T val)
		{
			Value = val;
		}
	}
	public class Graph<T, Node>
	{
		public int Count { get; private set; }
		public Dictionary<T, Node<T>> Nodes;

		public Graph (int count)
		{
			Count = count;
			Nodes = new Dictionary<T, Node<T>> (count);
		}

		public void AddNode(Node<T> node)
		{
			if (Nodes.Count == Count) {
				throw new OverflowException ("No more than " + Count + " nodes allowed.");
			}
			Nodes [node.Value] = node;
			Count++;
		}

		public static bool IsRoot(Node<T> node)
		{
			return null == node.Parent;
		}

		public Node<T> GetRoot()
		{
			return Nodes.Values.FirstOrDefault (node => IsRoot(node));
		}
	}
}

