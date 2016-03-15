using System;
using Wintellect.PowerCollections;

namespace Editor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var text = new BigList<string> ();
			string input = " ";
			do {
				input = Console.ReadLine();

				string command;
				string arguments;

				if( -1 == input.IndexOf( ' ' )) {
					command = input.Substring(0, input.IndexOf(' ')).ToLower();
					arguments = input.Substring(input.IndexOf(' ')+1);
				} else {
					command = input;
					arguments = " ";
				}
				switch( command ) {
					case "append":
					text.Add(arguments);
					Console.WriteLine("OK");
					break;

					case "insert":
					var subText = arguments.Substring(0, arguments.LastIndexOf(' '));
					var pos = int.Parse(arguments.Substring(arguments.LastIndexOf(' ')+1));
					text.Insert(pos, subText);
					Console.WriteLine("OK");

					break;

					case "delete":
					var parts = arguments.Split( ' ' );
					try {
						text.RemoveRange(int.Parse(parts[1]), int.Parse(parts[2]));
						Console.WriteLine("OK");
					}
					catch(Exception) {
						Console.WriteLine("ERROR");
					}
					break;
				case "print":
					text.ForEach(Console.Write);
					Console.WriteLine ();
					break;
				}

			} while("end" != input.ToLower());
		}
	}
}
