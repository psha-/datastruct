using System;
using System.Linq;
using System.Collections.Generic;
namespace TreeStruct
{

	public class Tree
	{
		public int Value;
		public IList<Tree> Children;
		public Tree Parent;
		private static int MaxDepth;

		static Dictionary<int, Tree> nodeByValue = new Dictionary<int, Tree> ();

		public Tree(int v, params Tree[] children)
		{
			Value = v;
			this.Children = new List<Tree>(children.Length);
			foreach (var child in children)
			{
				child.Parent = this;
				this.Children.Add(child);
			}

		}

		public static bool IsRoot(Tree node)
		{
			return null == node.Parent;
		}

		public static bool IsLeaf(Tree node)
		{
			return 0 == node.Children.Count;
		}

		public static bool IsMiddleNode(Tree node)
		{
			return !IsRoot(node) && !IsLeaf(node);
		}

		public static Tree GetNodeByValue(int val)
		{
			if( !nodeByValue.ContainsKey(val) ) {
				nodeByValue [val] = new Tree(val);
			}
			return nodeByValue [val];
		}

		public static Tree GetRootNode()
		{
			return nodeByValue.Values.FirstOrDefault (node => IsRoot(node));
		}

		public static List<Tree> GetLeafNodes()
		{
			return nodeByValue.Values.Where (node => IsLeaf(node)).ToList();
		}

		public static List<Tree> GetMidNodes()
		{
			return nodeByValue.Values.Where (node => IsMiddleNode(node)).ToList();
		}

		public static void GetFurthestNode(ref Tree FurthestNode, Tree parent, int depth=0)
		{
			if( depth > MaxDepth ) {
				MaxDepth = depth;
				FurthestNode = parent;
			}
			foreach (var child in parent.Children) {
				GetFurthestNode(ref FurthestNode, child, depth+1);
			}
		}

		public static void GetSumPath(int sum, ref List<Tree> CorrectLeafs, Tree node)
		{
			sum -= node.Value;
			if (IsLeaf( node ) ) {
				if (0 == sum) {
					CorrectLeafs.Add (node);
				}
			} else {
				foreach (var child in node.Children) {
					GetSumPath (sum, ref CorrectLeafs, child);
				}
			}
		}

		public static int GetSumSubtree(int sum, ref List<Tree> CorrectParents, Tree node)
		{
			int SubtreeSum = node.Value;
			foreach (var child in node.Children) {
				SubtreeSum += GetSumSubtree (sum, ref CorrectParents, child);
			}
			if (sum == SubtreeSum) {
				CorrectParents.Add (node);
			}
			return SubtreeSum;
		}

		public void DFSEach(Action<int> action)
		{
			action(this.Value);
			foreach (var child in this.Children) {
				child.DFSEach (action);
			}
		}

		public static int PrintPathTo(Tree node)
		{
			int depth = 0;
			var path = new Stack<int>();
			while (null != node) {
				path.Push (node.Value);
				node = node.Parent;
				depth++;
			}
			while( path.Count > 1 ) {
				Console.Write("{0} -> ", path.Pop());
			}
			Console.Write ("{0}", path.Pop ());
			return depth;
		}
	}
}