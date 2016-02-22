using System;

namespace LinearStruct
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Sum and avararge
			Console.WriteLine ("Sum and Average. Enter space separated integers: ");
			try {
				var ints = new Integers (Console.ReadLine());
				Console.WriteLine ("Sum={0:G}; Average={1:G}", ints.Sum(), ints.Avg());
			}
			catch(FormatException e) {
				Console.WriteLine (e.Message);
			}



			// Sort words
			Console.WriteLine ("Sorting words. Enter space separated words: ");
			{
				var words = new Words (Console.ReadLine());
				Console.WriteLine (words.Sort());
			}



			// Longest subsequence
			Console.WriteLine ("Longest subsequence. Enter space separated integers: ");
			try {
				var ints = new Integers (Console.ReadLine());
				ints.LongestSeq().ForEach(i => Console.Write("{0} ", i));

			}
			catch(FormatException e) {
				Console.WriteLine (e.Message);
			}



			// Longest subsequence
			Console.WriteLine ("Remove odd occurences. Enter space separated integers: ");
			try {
				var ints = new Integers (Console.ReadLine());
				ints.RemoveOddOcc().ForEach(i => Console.Write("{0} ", i));

			}
			catch(FormatException e) {
				Console.WriteLine (e.Message);
			}




		}
	}
}
