using System;
using System.Collections.Generic;

namespace StacksQueues
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			ConsoleKeyInfo Key;
			do {
				Console.Clear();
				Console.WriteLine ("Select:");
				Console.WriteLine ("1. Reversed numbers");
				Console.WriteLine ("2. Number sequence S, S+1, 2*S+1, S+2");
				Console.WriteLine ("q - Quit");

				Key = Console.ReadKey ();
				Console.WriteLine();
				switch (Key.KeyChar) {

				case '1':
					// Reversed numbers
					Console.WriteLine ("Reversed numbers. Write a space separated integers.");
					var numbers = new Stack<int>();
					try
					{
						foreach (var number in Console.ReadLine ().Split()) {
							numbers.Push (Int32.Parse (number));
						}
					}
					catch( FormatException e ) {
						Console.WriteLine(e.Message);
					}
					while( numbers.Count > 0 ) {
						Console.Write("{0} ", numbers.Pop());
					}

					break;

				case '2':
					// Number sequence
					Console.WriteLine ("Number sequence S, S+1, 2*S+1, S+2. Printing the first 50 elements. Enter first element:");
					try {
						int first = Int32.Parse( Console.ReadLine() );
						var seq = new ANumberSequence(first, 50);
						foreach( int item in seq )
						{
							Console.Write("{0} ", item);
						}
						Console.WriteLine();
					}
					catch( FormatException e ) {
						Console.WriteLine(e.Message);
					}


					break;

				}// switch
				Console.ReadKey ();
			}// do
			while(Key.KeyChar != 'q');
		}// Main
	}// MainClass
}//StacksQueues
