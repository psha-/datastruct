using System;
using System.Linq;
using System.Collections.Generic;

namespace TreeStruct
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter nodes count:");

			int count = 0;
			try {
				count = Int32.Parse(Console.ReadLine ());
			}
			catch(FormatException e) {
				Console.WriteLine(e.Message);
			}

			Tree node, parent;
			for (var i=0; i< count; i++) {
				Console.WriteLine ("Enter pair {0}:", i+1);
				int[] pair = Console.ReadLine ().Split ().Select (h => Int32.Parse (h)).ToArray ();
				if (pair.Length >= 1) {
					parent = Tree.GetNodeByValue (pair [0]);
					if (pair.Length >= 2) {
						node = Tree.GetNodeByValue (pair [1]);
						parent.Children.Add (node);
						node.Parent = parent;
					}
				}
			}
			Console.WriteLine ("Enter path sum:");
			var pathSum = Int32.Parse(Console.ReadLine ());

			Console.WriteLine ("Enter subtree sum:");
			var subtreeSum = Int32.Parse(Console.ReadLine ());

			Console.WriteLine("Root node: {0}", Tree.GetRootNode().Value);

			Console.Write("Leaf nodes:");
			Tree.GetLeafNodes().ForEach(n => Console.Write(" {0}", n.Value));
			Console.WriteLine();

			Console.Write("Middle nodes:");
			Tree.GetMidNodes().ForEach(n => Console.Write(" {0}", n.Value));
			Console.WriteLine();

			Console.Write("Longest path: ");
			Tree.PrintPathTo (Tree.GetFurthesNode ());

			Console.WriteLine("Path of sum {0}: ", pathSum);
			var sumLeafs = new List<Tree> ();
			Tree.GetSumPath (pathSum, ref sumLeafs, Tree.GetRootNode());
			sumLeafs.ForEach(n => Tree.PrintPathTo (n));

			Console.WriteLine("Subtrees of sum {0}: ", pathSum);
			var sumParents = new List<Tree> ();
			int initSum = 0;
			Tree.GetSumSubtree (subtreeSum, ref initSum, ref sumParents, Tree.GetRootNode ());
			sumParents.ForEach (n => Tree.PrintSum (n));

		}
	}
}
