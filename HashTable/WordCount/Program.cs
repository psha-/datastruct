using System;
using System.Collections.Generic;
using HashTable;

namespace WordCount
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter some text");
			var input = Console.ReadLine ();
			var charsDictionary = new HashTable<string, int>(100);
			var charStrings = new List<string>(input.Length);
			foreach(var aChar in input) {
				charStrings.Add(aChar.ToString());
			}
			foreach (var charString in charStrings) {
				if (charsDictionary.ContainsKey (charString)) {
					charsDictionary [charString]++;
				} else {
					charsDictionary [charString] = 1;
				}
			}
			foreach (var charString in charsDictionary) {
				Console.WriteLine ("{0}: {1} time/s", charString.Key, charString.Value);
			}
		}
	}
}
