using System;
using Wintellect.PowerCollections;

namespace Editor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var text = new BigList<char> ();
			string input = " ";
			do {
				input = Console.ReadLine();

				string command;
				string arguments;

				if( -1 != input.IndexOf( ' ' )) {
					command = input.Substring(0, input.IndexOf(' ')).ToLower();
					arguments = input.Substring(input.IndexOf(' ')+1);
				} else {
					command = input;
					arguments = " ";
				}
				switch( command ) {
					case "append":
					    text.AddRange(arguments.ToCharArray());
					    Console.WriteLine("OK");
					    break;

					case "insert":
                        var pos = int.Parse(arguments.Substring(0, arguments.IndexOf(' ')));
                        arguments = arguments.Substring(arguments.IndexOf(' ') + 1);
                        try
                        {
                            text.InsertRange(pos, arguments);
                            Console.WriteLine("OK");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERROR");
                        }
                        break;

					case "delete":
					    var parts = arguments.Split( ' ' );
					    try {
					    	text.RemoveRange(int.Parse(parts[0]), int.Parse(parts[1]));
					    	Console.WriteLine("OK");
					    }
					    catch(Exception) {
					    	Console.WriteLine("ERROR");
					    }
					    break;

                    case "replace":
                        var replaceStart = int.Parse(arguments.Substring(0, arguments.IndexOf(' ')));
                        arguments = arguments.Substring(arguments.IndexOf(' ')+1);
                        var replaceCount = int.Parse(arguments.Substring(0, arguments.IndexOf(' ')));
                        arguments = arguments.Substring(arguments.IndexOf(' ') + 1);
                        try
                        {
                            text.RemoveRange(replaceStart, replaceCount);
                            text.InsertRange(replaceStart, arguments.ToCharArray());
                            Console.WriteLine("OK");
                        } catch (Exception)
                        {
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
