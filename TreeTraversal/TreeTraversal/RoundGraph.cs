using System;
using System.Collections.Generic;

namespace TreeTraversal
{
	class RoundGraph<T, Node>: Graph<T, Node> {
		private T LeaderValue;

		public RoundGraph(T leaderVal) {
			LeaderValue = leaderVal;
			Nodes = new Dictionary<T, Node<T>> ();
		}

		public int GetLongestDance()
		{
			visited = new Dictionary<T, bool> ();
			int depth = 1;
			var FurthestNode = Nodes [LeaderValue];
			GetFurthestNode (ref FurthestNode, FurthestNode, ref depth, 1);

			return depth;
		}
	}

}

