using System;
using System.Collections.Generic;
using HashTable;

namespace WordCount
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			Console.WriteLine ("Phonebook. Available commands:");
			Console.WriteLine ("");
			Console.WriteLine ("- a [name]|[phone] : Adds new contact.");
			Console.WriteLine ("- s [name] : Searches for specific name.");
			Console.WriteLine ("- q : quit.");
			Console.WriteLine ("");
			var phoneBook = new HashTable<string, string> (50);
			char command;
			do {
				string input = Console.ReadLine();
				command = input[0];
				if(input.Length<=2) {
					continue;
				}
				var arguments = input.Substring(2);
				switch(command) {
				case 'a':
					var pair = arguments.Split('|');
					phoneBook[pair[0]] = pair[1];
					break;
				case 's':
					if( phoneBook.ContainsKey(arguments)) {
						Console.WriteLine("{0} -> {1}", arguments, phoneBook[arguments]);
					} else {
						Console.WriteLine("Contact {0} does not exist.", arguments);
					}
					break;
				}

			} while ('q' != command);
		}
	}
}
