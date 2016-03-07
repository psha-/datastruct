using System;
using System.Collections.Generic;

namespace TreeTraversal
{
	class LimitedGraph<T, Node>: Graph<T, Node> {
		public int MaxCount { get; private set; }

		public LimitedGraph (int max)
		{
			MaxCount = max;
			Nodes = new Dictionary<T, Node<T>> (max);
		}

		public override void AddPair(T nodeVal, T parentVal)
		{
			if (Nodes.Count == MaxCount && (!Nodes.ContainsKey (nodeVal) || !Nodes.ContainsKey (parentVal))) {
				throw new OverflowException ("No more than " + MaxCount + " nodes allowed.");
			}
			base.AddPair (nodeVal, parentVal);
		}
	}
}

