using System;
using Wintellect.PowerCollections;

namespace DSCollections
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			var products = new OrderedMultiDictionary<float, string> (true);
			Console.WriteLine("Add 'product price'. 'q' for end. ");
			while(true) {
				var input = Console.ReadLine ();
				if ("q" == input) {
					break;
				}
				var pair = input.Split (' ');
				try {
					products.Add (float.Parse (pair [1]), pair [0]);
				} catch (FormatException e) {
					Console.WriteLine (e.Message);
				}
			}
			Console.WriteLine("Add range 'price1 price2'. 'q' for end. ");
			while (true) {
				var input = Console.ReadLine ();
				if ("q" == input) {
					break;
				}
				var range = input.Split (' ');
				try {
					Console.WriteLine(products.Range (float.Parse(range [0]), true, float.Parse(range [1]), true));
				} catch (FormatException e) {
					Console.WriteLine (e.Message);
				}
			}

		}
	
	}
}
