using System;
using System.Linq;

namespace TreeTraversal
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Finding the root.");
			Console.WriteLine ("Number of nodes:");
			var cNodes = Console.ReadLine ();
			Console.WriteLine ("Number of edges:");
			var cEdges = Console.ReadLine ();
			var graph = new Graph<int, Node<int>> (int.Parse(cNodes));
			try{
				for (var i=0; i<int.Parse(cEdges); i++) {
					int[] pair = Console.ReadLine ().Split ().Select (h => int.Parse (h)).ToArray ();
					var parent = new Node<int> (pair [0]);
					var child = new Node<int> (pair [1]);
					child.Parent = parent;
					try {
						graph.AddNode (parent);
						graph.AddNode (child);
					}
					catch( OverflowException e) {
						Console.WriteLine (e.Message);
					}
				}
				Console.WriteLine("The root is: {0}", graph.GetRoot ());
			}
			catch(FormatException e) {
				Console.WriteLine (e.Message);
			}
		}
	}
}
