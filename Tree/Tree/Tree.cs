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

		public static Tree GetNodeByValue(int val)
		{
			if( !nodeByValue.ContainsKey(val) ) {
				nodeByValue [val] = new Tree(val);
			}
			return nodeByValue [val];
		}

		public static Tree GetRootNode()
		{
			return nodeByValue.Values.FirstOrDefault (node => node.Parent == null);
		}

		public static List<Tree> GetLeafNodes()
		{
			return nodeByValue.Values.Where (node => 0 == node.Children.Count ).ToList();
		}

		public static List<Tree> GetMidNodes()
		{
			return nodeByValue.Values.Where (node => node.Children.Count > 0 && null != node.Parent).ToList();
		}

		public static void GetSumSubtree(int sum, ref int SubtreeSum, ref List<Tree> CorrectParents, Tree node)
		{
			SubtreeSum += node.Value;
			int ChildrenSum = 0;
			foreach (var child in node.Children) {
				GetSumSubtree (sum, ref ChildrenSum, ref CorrectParents, child);
			}
			if (sum == node.Value + ChildrenSum) {
				CorrectParents.Add (node);
			}
		}

		public static void GetSumPath(int sum, ref List<Tree> CorrectLeafs, Tree node)
		{
			sum -= node.Value;
			if (0 == node.Children.Count ) {
				if (0 == sum) {
					CorrectLeafs.Add (node);
				}
			} else {
				foreach (var child in node.Children) {
					GetSumPath (sum, ref CorrectLeafs, child);
				}
			}
		}

		public static void PrintSum(Tree node)
		{
			Console.Write ("{0} ", node.Value);
			foreach (var child in node.Children) {
				PrintSum (child);
			}
		}

		public static void PrintPathTo(Tree node)
		{
			var path = new Stack<int>();
			while (null != node) {
				path.Push (node.Value);
				node = node.Parent;
			}
			while( path.Count > 1 ) {
				Console.Write("{0} -> ", path.Pop());
			}
			Console.WriteLine ("{0}", path.Pop ());
		}

		public static Tree GetFurthesNode()
		{
			var nodes = new Stack<Tree> ();
			var depth = 0;
			Tree FurthestNode = GetRootNode();

			nodes.Push (FurthestNode);
			while (nodes.Count > 0) {
				var node = nodes.Pop ();
				if (0 == node.Children.Count ) {
					if (nodes.Count > depth) {
						depth = nodes.Count;
						FurthestNode = node;
					}
				} else {
					foreach (var child in node.Children) {
						nodes.Push (child);
					}
				}
			}
			return FurthestNode;
		}

	}
}