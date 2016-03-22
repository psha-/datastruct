using System;

namespace Products
{
	public class Product
	{
		public int Id{ get; private set;}
		public string Title{ get; private set;}
		public string Supplier{ get; private set;}
		public float Price{ get; private set;}

		public Product(int id, string title, string supplier, float price)
		{
			Id = id;
			Title = title;
			Supplier = supplier;
			Price = price;
		}

		public override string ToString ()
		{
			return string.Format ("[{0},{1},{2},{3}]\n", Id, Title, Supplier, Price);
		}
	}
}

