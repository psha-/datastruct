using System;
using System.Threading;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace SpecialCollections
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			//Courses ();
			Editor ();


		}
	
		public static void Courses() {
			var courses = new OrderedMultiDictionary<DateTime, string> (true);
			Console.WriteLine ("Number of courses:");
			int num = int.Parse(Console.ReadLine ());
			for( var i=0; i< num; i++) {
				var pair = Console.ReadLine ().Split ('|');
				courses.Add (DateTime.Parse (pair [1]), pair [0]);
			}
			Console.WriteLine ("Number of ranges:");
			int rangesNum = int.Parse (Console.ReadLine ());
			var ranges = new List<List<string>>();
			for (var i=0; i< rangesNum; i++) {
				var input = Console.ReadLine ().Split ('|');
				ranges.Add (new List<string> { input [0], input[1] });
			}
			for (var i=0; i< rangesNum; i++) {
				Console.WriteLine (courses.Range (DateTime.Parse (ranges[i][0]), true, DateTime.Parse (ranges[i][1]), true));
			}

		}

		public static void Editor()
		{
			var text = new BigList<string> ();
			string command = " ";
			do {
				command = Console.ReadLine();
				var parts = command.Split(' ');

				switch( parts[0]) {
				case "INSERT":
					text.Add(parts[1]);
					Console.WriteLine("OK");
					break;

				case "APPEND":
					text.AddToFront(parts[1]);
					Console.WriteLine("OK");

					break;

				case "DELETE":
					try {
						text.RemoveRange(int.Parse(parts[1]), int.Parse(parts[2]));
		                Console.WriteLine("OK");
	                }
	                catch(Exception) {
						Console.WriteLine("ERROR");
					}
					break;
				}

			} while("PRINT" != command);
			Console.WriteLine (text);

		}

	}
}
