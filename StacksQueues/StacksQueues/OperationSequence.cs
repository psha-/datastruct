using System;
using System.Collections;
using System.Collections.Generic;

namespace StacksQueues
{
	public class OperationSequence//: IEnumerable
	{
		private class Node
		{
			public int Value { get; private set; }
			public Node Prev;

			public Node(int val, Node pre = null)
			{
				Value = val;
				Prev = pre;
			}
		}

		private int Start;
		private int End;
		private Queue<Node> sequence;

		public OperationSequence (int s, int e)
		{
			Start = s;
			End = e;

			sequence = new Queue<Node> ();
		}

		public void CalcPrint ()
		{
			var node = new Node(Start);
			bool found = false;
			sequence.Enqueue (node);
			while (sequence.Count > 0) {
				node = sequence.Dequeue ();

				if (node.Value < End) {
					sequence.Enqueue (new Node (node.Value + 1, node));
					sequence.Enqueue (new Node (node.Value + 2, node));
					sequence.Enqueue (new Node (node.Value * 2, node));
				}
				if (node.Value == End) {
					found = true;
					while (node != null) {
						Console.Write (" <- {0} ", node.Value);
						node = node.Prev;
					}
					Console.WriteLine ();
					break;
				}
			}
			if (!found) {
				Console.WriteLine ("(no solution)");
			}
		}
	}
}

