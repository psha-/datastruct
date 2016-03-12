using System;

namespace OrderedSet
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var ints = new OrderedSet<int> ();

			Console.WriteLine ("Initial tree:");
			ints.Add (17);
			ints.Add (17);
			ints.Add (17);
			ints.Add (9);
			ints.Add (12);
			ints.Add (19);
			ints.Add (6);
			ints.Add (25);

			foreach (var val in ints) {
				Console.WriteLine (val);
			}
			Console.WriteLine ("17 exist. {0}.", ints.Contains(17));
			Console.WriteLine ("9 exist. {0}.", ints.Contains(9));
			Console.WriteLine ("12 exist. {0}.", ints.Contains(12));
			Console.WriteLine ("18 exists. {0}.", ints.Contains(18));
			Console.WriteLine ("19 exists. {0}.", ints.Contains(19));
			Console.WriteLine ("6 exists. {0}.", ints.Contains(6));
			Console.WriteLine ("25 exists. {0}.", ints.Contains(25));
			Console.WriteLine ("27 exists. {0}.", ints.Contains(27));
			Console.WriteLine ("28 exists. {0}.", ints.Contains(28));


			Console.WriteLine ("Adding some elements:");
			ints.Add (1);
			ints.Add (2);
			ints.Add (3);
			ints.Add (4);
			ints.Add (5);
			ints.Add (7);
			ints.Add (8);
			ints.Add (10);
			foreach (var val in ints) {
				Console.WriteLine (val);
			}


			Console.WriteLine ("Removing some elements:");
			ints.Remove (2);
			ints.Remove (5);
			ints.Remove (6);
			ints.Remove (9);
			ints.Remove (17);
			ints.Remove (100);

			foreach (var val in ints) {
				Console.WriteLine (val);
			}
		}
	}
}
