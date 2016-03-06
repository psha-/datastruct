using System;
using System.Linq;

namespace TreeTraversal
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ConsoleKeyInfo key;
			do {
				Console.Clear();
				Console.WriteLine("1 - Finding the root.");
				Console.WriteLine("2 - Round dance.");
				Console.WriteLine("q - Quit");
				key = Console.ReadKey ();
				Console.WriteLine();
				switch( key.KeyChar) {
					case '1':
					Console.WriteLine ("Finding the root.");
					Console.WriteLine ("Enter number of nodes:");
					try {
						int cNodes = int.Parse (Console.ReadLine ());
						Console.WriteLine ("Enter number of edges:");
						int cEdges = int.Parse(Console.ReadLine ());
						Graph<int, Node<int>> plainGraph = new LimitedGraph<int, Node<int>> (cNodes);
						Console.WriteLine ("Enter nodes:");
						for (var i=0; i<cEdges; i++) {
							int[] pair = Console.ReadLine ().Split ().Select (h => int.Parse (h)).ToArray ();
							try {
								plainGraph.AddPair(pair[1], pair[0]);
							} catch (OverflowException e) {
								Console.WriteLine (e.Message);
							}
						}
						plainGraph.PrintRoot();
					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}
					break;


				case '2':
					Console.WriteLine("Round dance.");
					Console.WriteLine("Enter number of friendships:");
					try {
						int fCount = int.Parse(Console.ReadLine());
						Console.WriteLine("Enter leader number:");
						int leader = int.Parse(Console.ReadLine());
						Console.WriteLine("Enter friendsips:");
						RoundGraph<int, Node<int>> roundDance = new RoundGraph<int, Node<int>> (leader);
						for( var i=0; i<fCount; i++) {
							int[] pair = Console.ReadLine ().Split ().Select (h => int.Parse (h)).ToArray ();
							roundDance.AddPair(pair[0], pair[1]);
							roundDance.AddPair(pair[1], pair[0]);
						}
						Console.WriteLine("The longest round dance has {0} people", roundDance.GetLongestDance());
					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}
					catch (IndexOutOfRangeException e) {
						Console.WriteLine (e.Message);
					}
					break;
				}
				Console.ReadKey();
			}// do
			while( 'q' != key.KeyChar );
		}
	}
}
