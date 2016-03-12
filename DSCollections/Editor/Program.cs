using System;
using Wintellect.PowerCollections;

namespace Editor
{
	class MainClass
	{
		public static void Main (string[] args)
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
				case "PRINT":
					Console.WriteLine (text);
					break;
				}

			} while("END" != command);
		}
	}
}
