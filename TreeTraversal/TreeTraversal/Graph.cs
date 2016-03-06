using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeTraversal
{
	public class Node<T>
	{
		public T Value { get; private set; }
		public List<Node<T>> Parents;
		public List<Node<T>> Children;
		public Node(T val)
		{
			Value = val;
			Parents = new List<Node<T>> ();
			Children = new List<Node<T>> ();
		}
	}

	public class Graph<T, Node>
	{
		public Dictionary<T, Node<T>> Nodes;
		protected static Dictionary<T, bool> visited;

		public Graph ()
		{
			Nodes = new Dictionary<T, Node<T>> ();
		}

		public virtual void AddPair( T nodeVal, T parentVal ) {
			Node<T> parent;
			Node<T> node;

			if (!Nodes.ContainsKey (parentVal)) {
				parent = new Node<T> (parentVal);
				Nodes [parentVal] = parent;
			} else {
				parent = Nodes [parentVal];
			}

			if (!Nodes.ContainsKey (nodeVal)) {
				node = new Node<T> (nodeVal);
				Nodes [nodeVal] = node;
			} else {
				node = Nodes [nodeVal];
			}
			node.Parents.Add (parent);
			parent.Children.Add (node);

		}

		public static bool IsRoot(Node<T> node)
		{
			return 0 == node.Parents.Count;
		}

		public static bool IsLeaf(Node<T> node)
		{
			return 0 == node.Children.Count;
		}

		public List<T> GetRoot()
		{
			List<T> roots = new List<T>();
			foreach (var node in Nodes.Values) {
				if (IsRoot (node)) {
					roots.Add (node.Value);
				}
			}
			return roots;
		}

		public void PrintRoot()
		{
			var roots = GetRoot ();
			switch (roots.Count) {
			case 0:
				Console.WriteLine ("No root!");
				break;
			case 1:
				Console.Write ("The root is: {0}", string.Join(", ", GetRoot().ToArray()));
				break;
			default:
				Console.WriteLine ("Multiple root nodes!");
				break;
			}
		}

		public static void GetFurthestNode( ref Node<T> FurthestNode, Node<T> parent, ref int maxDepth, int depth=0 )
		{
			if (visited.ContainsKey(parent.Value)) {
				return;
			}
			visited [parent.Value] = true;
			if( depth > maxDepth ) {
				maxDepth = depth;
				FurthestNode = parent;
			}
			foreach (var child in parent.Children) {
				GetFurthestNode (ref FurthestNode, child, ref maxDepth, depth+1);
			}
		}

	}

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

	class TreeInt<Node>: LimitedGraph<int, Node> {

		public TreeInt(int max):base(max)
		{

		}

		public override void AddPair(int nodeVal, int parentVal)
		{
			base.AddPair (nodeVal, parentVal);
			if (Nodes [nodeVal].Parents.Count > 1) {
				throw new InvalidOperationException ("Tree nodes can have only one parent");
			}
		}

		private int SumPathToRoot(Node<int> node)
		{
			var sum = 0;
			while (!IsRoot(node)) {
				sum += node.Value;
				node = node.Parents [0];
			}
			return sum+node.Value;
		}

		private int SumPath(Node<int> fromNode, Node<int> toNode)
		{
			return SumPathToRoot (fromNode) - SumPathToRoot (toNode) + toNode.Value;
		}

		public int CalcLongestLeafPath()
		{
			int maxSum = 0;
			foreach (var node1 in Nodes) {
				foreach (var node2 in Nodes) {
					if( IsLeaf(node1.Value) && IsLeaf(node2.Value) && node1.Value != node2.Value) {
						var sum = SumPath(node1.Value, node2.Value);
						if (sum > maxSum) {
							maxSum = sum;
						}
					}
				}
			}
			return maxSum;
		}

	}
}

