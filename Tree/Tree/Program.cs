using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace TreeStruct
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			ConsoleKeyInfo key;

			do {
				Console.Clear ();
				Console.WriteLine ("1 - Tree");
				Console.WriteLine ("2 - Filesystem");
				Console.WriteLine ("3 - Calculations");
				Console.WriteLine ("q - Quit");
				key = Console.ReadKey ();
				Console.WriteLine();
				switch (key.KeyChar) {
				case '1':
					Console.WriteLine ("Enter nodes count:");

					int count = 0;
					try {
						count = Int32.Parse (Console.ReadLine ());
					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}

					Tree node, parent;
					for (var i=0; i< count; i++) {
						Console.WriteLine ("Enter pair {0}:", i + 1);
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
					var pathSum = Int32.Parse (Console.ReadLine ());

					Console.WriteLine ("Enter subtree sum:");
					var subtreeSum = Int32.Parse (Console.ReadLine ());

					Console.WriteLine ("Root node: {0}", Tree.GetRootNode ().Value);
					Console.WriteLine ();

					Console.Write ("Leaf nodes:");
					Tree.GetLeafNodes ().ForEach (n => Console.Write (" {0}", n.Value));
					Console.WriteLine ();
					Console.WriteLine ();

					Console.Write ("Middle nodes:");
					Tree.GetMidNodes ().ForEach (n => Console.Write (" {0}", n.Value));
					Console.WriteLine ();
					Console.WriteLine ();

					Console.Write ("Longest path: ");
					Tree FurthestNode = Tree.GetRootNode ();
					Tree.GetFurthestNode (ref FurthestNode, FurthestNode);
					var depth = Tree.PrintPathTo (FurthestNode);
					Console.WriteLine ("(length =  {0})", depth);
					Console.WriteLine ();

					Console.WriteLine ("Path of sum {0}: ", pathSum);
					var sumLeafs = new List<Tree> ();
					Tree.GetSumPath (pathSum, ref sumLeafs, Tree.GetRootNode ());
					sumLeafs.ForEach (n => Tree.PrintPathTo (n));
					Console.WriteLine ();

					Console.WriteLine ("Subtrees of sum {0}: ", subtreeSum);
					var sumParents = new List<Tree> ();
					Tree.GetSumSubtree (subtreeSum, ref sumParents, Tree.GetRootNode ());
					foreach (var sumParent in sumParents) {
						var sum = new List<int> ();
						sumParent.DFSEach (sum.Add);
						Console.WriteLine (String.Join (" + ", sum.ToArray ()));
					}
					break;
				case '2':
					Console.WriteLine("Give a starting path:");
					var path = Console.ReadLine();

					try {
						DirectoryInfo di = new DirectoryInfo(@path);

						var root = Folder.TraverseFolder(di);

						Console.WriteLine("Give a subpath to calculate size:");
						var subpath = Console.ReadLine();
						var subfolder = Folder.Folders[subpath]; 
						Console.WriteLine("The size is: {0:n0}K", Folder.CalculateSize(subfolder)/1024);
					}
					catch(DirectoryNotFoundException e) {
						Console.WriteLine(e.Message);
					}
					catch(ArgumentException e) {
						Console.WriteLine(e.Message);
					}
					catch(KeyNotFoundException) {
						Console.WriteLine("Path not found in the tree.");
					}
					break;
				}
				Console.ReadKey();
			}// do
			while ('q' != key.KeyChar);
		}
	}
}
