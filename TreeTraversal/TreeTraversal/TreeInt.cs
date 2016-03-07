using System;
using System.Collections.Generic;

namespace TreeTraversal
{
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

		public static Node<int> GetRootChildOfNode(Node<int> node)
		{
			if (0 == node.Parents.Count) {
				return null;
			}
			while (0 != node.Parents[0].Parents.Count) {
				node = node.Parents [0];
			}
			return node;
		}

		public static void GetGreatestSumLeaf( ref Node<int> Leaf, Node<int> parent, ref int greatestSum, Node<int> forbidden = null, int sum=0 )
		{
			if (null != forbidden && parent == forbidden) {
				return;
			}

			sum += parent.Value;
			if (sum > greatestSum) {
				greatestSum = sum;
				if (IsLeaf (parent)) {
					Leaf = parent;
				}
			}
			foreach (var child in parent.Children) {
				GetGreatestSumLeaf (ref Leaf, child, ref greatestSum, forbidden, sum);
			}
		}

		public int CalcLongestLeafPath()
		{
			int greatestSum=int.MinValue;

			foreach (var pair in Nodes) {
				if (!IsLeaf (pair.Value) && pair.Value.Children.Count > 1) {
					int firstHalfSum=int.MinValue;
					int secondHalfSum=int.MinValue;
					Node<int> firstHalfLeaf = pair.Value;
					Node<int> secondHalfLeaf = pair.Value;
					GetGreatestSumLeaf (ref firstHalfLeaf, pair.Value, ref firstHalfSum);
					GetGreatestSumLeaf (ref secondHalfLeaf, pair.Value, ref secondHalfSum, GetRootChildOfNode(firstHalfLeaf));
					int sum = firstHalfSum + secondHalfSum - pair.Value.Value;
					if( sum > greatestSum ) {
						greatestSum = sum;
					}
				}
			}
			return greatestSum;
		}


	}

}

