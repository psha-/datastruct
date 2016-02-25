using System;

namespace LinearStruct
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ConsoleKeyInfo Key;
			do {
				Console.Clear();
				Console.WriteLine ("Select:");
				Console.WriteLine ("1. Sum and average");
				Console.WriteLine ("2. Sorting words");
				Console.WriteLine ("3. Longest subsequence");
				Console.WriteLine ("4. Remove odd occurences");
				Console.WriteLine ("5. Count occurences");
				Console.WriteLine ("6. Reversed list");
				Console.WriteLine ("7. Linked list");
				Console.WriteLine ("8. Calcualting distance in labyrinth.");
				Console.WriteLine ("q - Quit");

				Key = Console.ReadKey ();
				Console.WriteLine();
				switch (Key.KeyChar) {

				case '1':
						// Sum and avararge
					Console.WriteLine ("Sum and Average. Enter space separated integers: ");
					try {
						var ints = new Integers (Console.ReadLine ());
						Console.WriteLine ("Sum={0:G}; Average={1:G}", ints.Sum (), ints.Avg ());
					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}
					break;


				case '2':
						// Sort words
					Console.WriteLine ("Sorting words. Enter space separated words: ");
					{
						var words = new Words (Console.ReadLine ());
						Console.WriteLine (words.Sort ());
					}
					break;


				case '3':
						// Longest subsequence
					Console.WriteLine ("Longest subsequence. Enter space separated integers: ");
					try {
						var ints = new Integers (Console.ReadLine ());
						ints.LongestSeq ().ForEach (i => Console.Write ("{0} ", i));
						Console.Write ("\n");

					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}
					break;


				case '4':	
						// Remove odd occurences
					Console.WriteLine ("Remove odd occurences. Enter space separated integers: ");
					try {
						var ints = new Integers (Console.ReadLine ());
						ints.RemoveOddOcc ().ForEach (i => Console.Write ("{0} ", i));
						Console.Write ("\n");

					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					}
					break;


				case '5':		
						// Count occurences
					Console.WriteLine ("Count occurences. Enter space separated integers between 0 and 1000: ");
					try {
						var ints = new Integers (Console.ReadLine (), 0, 1000);
						foreach (var pair in ints.CountOcc()) {
							Console.WriteLine ("{0} -> {1} times ", pair.Key, pair.Value);
						}

					} catch (FormatException e) {
						Console.WriteLine (e.Message);
					} catch (ArgumentOutOfRangeException e) {
						Console.WriteLine (e.Message);
					}

					break;



				case '6':
						// Reversed list
					Console.WriteLine ("Revered list. Adding elements 1,2,3,4 and changing index 0.");
					{
						var revList = new ReversedList<int> ();
						revList.Add (1);
						revList.Add (2);
						revList.Add (3);
						revList.Add (4);

						foreach (var item in revList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						revList.Remove (0);
						foreach (var item in revList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						revList.Add (4);
						foreach (var item in revList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");

						revList [0] = 44;
						foreach (var item in revList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						Console.Write (revList [0]);
						Console.Write ("\n");

					}
					break;


				case '7':
						// Linked list
					Console.WriteLine ("Lnked list. Adding ints 1,2,3,4,2,5,6. Removing index 5, 2 and 0.");
					{
						var linkedList = new LinkedList<int> ();
						linkedList.Add (1);
						linkedList.Add (2);
						linkedList.Add (3);
						linkedList.Add (4);
						linkedList.Add (2);
						linkedList.Add (5);
						linkedList.Add (6);

						foreach (var item in linkedList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						linkedList.Remove (5);
						foreach (var item in linkedList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						linkedList.Remove (2);
						foreach (var item in linkedList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						linkedList.Remove (0);
						foreach (var item in linkedList) {
							Console.Write ("{0} ", item);
						}
						Console.Write ("\n");
						Console.WriteLine ("Head {0}", linkedList.Head.val);
						Console.WriteLine ("Tail {0}", linkedList.Tail.val);
						Console.WriteLine ("First index of 2 is {0}", linkedList.FirstIndexOf (2));
						Console.WriteLine ("Last index of 2 is {0}", linkedList.LastIndexOf (2));

					}
					break;



				case '8':
					// Distance in labyrinth
					Console.WriteLine ("Calcualting distance in labyrinth.");

					{
						var map = new LabyrinthMap (new LabyrinthMap.mapItem[,] {
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x},
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x},
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o},
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o},
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o},
							{ LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x, LabyrinthMap.mapItem.o, LabyrinthMap.mapItem.x}
						});
						try {
							var start = new LabyrinthPos (1, 2);
							var labyrinth = new Labyrinth (map, start);
							labyrinth.map.Print ();
							var dst = labyrinth.calcDistances ();
							dst.MarkFreeAsU ();
							dst.Print ();

						} catch (AccessViolationException) {
							Console.WriteLine ("Invalid start position selected");
						}

					}
					break;


				}// switch
				Console.ReadKey ();
			}// do
			while (Key.KeyChar != 'q');
		}// Main
	}// MainClass
}// LinearStruct
