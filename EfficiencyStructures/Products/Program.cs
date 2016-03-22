using System;
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
			Console.WriteLine (products);
			try {
				Console.WriteLine("Price 0-10:");
				products.FindByPrice(0, 10);
				Console.WriteLine("Title Product1:");
				products.FindByTitle("Product1");
				Console.WriteLine("Title Product1, price 5:");
				products.FindByTitlePrice("Product1", 5);
				Console.WriteLine("Title Product1, price 0-10:");
				products.FindByTitlePrice("Product1", 0, 10);
				Console.WriteLine("Supplier Company10, price 5:");
				products.FindBySupplierPrice("Company10", 5);
				Console.WriteLine("Supplier Company10, price 0-10:");
				products.FindBySupplierPrice("Company10", 0, 10);
			} catch(KeyNotFoundException e) {

			}
		}
	}
}
