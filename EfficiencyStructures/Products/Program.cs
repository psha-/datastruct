using System;
using System.Collections;
using System.Collections.Generic;

namespace Products
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var products = new ProductCollection ();

			Random random = new Random();

			for (var i=1; i<=50; i++) {
				products.Add (new Product(i, "Product"+i, "Comapny"+i, random.Next(0, 20)));
				products.Add (new Product(i+50, "Product"+i, "Comapny"+i, random.Next(0, 20)));
			}
			Console.WriteLine("All products:");
			DisplayEnumerator(products.GetEnumerator());
			try {
				Console.WriteLine("\nPrice 0-10:");
				DisplayEnumerator( products.FindByPrice(0, 10) );
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}

			try {
				Console.WriteLine("\nTitle Product1:");
				DisplayEnumerator( products.FindByTitle("Product1"));
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}

			try {
				Console.WriteLine("\nTitle Product1, price 5:");
				DisplayEnumerator( products.FindByTitlePrice("Product1", 5));
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}

			try{
				Console.WriteLine("\nTitle Product1, price 0-10:");
				DisplayEnumerator( products.FindByTitlePrice("Product1", 0, 10));
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}

			try {
				Console.WriteLine("\nSupplier Company10, price 5:");
				DisplayEnumerator( products.FindBySupplierPrice("Company10", 5));
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}

			try {
				Console.WriteLine("\nSupplier Company10, price 0-10:");
				DisplayEnumerator( products.FindBySupplierPrice("Company10", 0, 10));
			} catch(KeyNotFoundException) {
				Console.WriteLine ("Products not found");
			}
		}

		public static void DisplayEnumerator(IEnumerator myEnumerator)
		{
			while (myEnumerator.MoveNext())
			{
				Console.WriteLine("{0}",
				myEnumerator.Current);
			}
		}
	}
}
