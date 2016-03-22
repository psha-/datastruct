using System;
using System.Collections.Generic;
using System.Text;

namespace BiDictionary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var distances = new BiDictionary<string, string, int>();
			distances.Add("Sofia", "Varna", 443);
			distances.Add("Sofia", "Varna", 468);
			distances.Add("Sofia", "Varna", 490);
			distances.Add("Sofia", "Plovdiv", 145);
			distances.Add("Sofia", "Bourgas", 383);
			distances.Add("Plovdiv", "Bourgas", 253);
			distances.Add("Plovdiv", "Bourgas", 292);

			PrintResult (distances.FindByKey1("Sofia")); // [443, 468, 490, 145, 383]
			PrintResult (distances.FindByKey2("Bourgas")); // [383, 253, 292]
			PrintResult (distances.Find("Plovdiv", "Bourgas")); // [253, 292]
			PrintResult (distances.Find("Rousse", "Varna")); // []
			PrintResult (distances.Find("Sofia", "Varna")); // [443, 468, 490]
			distances.Remove("Sofia", "Varna"); // true
			PrintResult (distances.FindByKey1("Sofia")); // [145, 383]
			PrintResult (distances.FindByKey2("Varna")); // []
			PrintResult (distances.Find("Sofia", "Varna")); // []
		}

		public static void PrintResult<T>(IEnumerable<T> result)
		{
			var line = new StringBuilder();
			foreach (var row in result) {
				line.AppendFormat("{0}, ", row);
			}
			if (line.Length > 0) {
				line.Length = line.Length - 2;
			}
			Console.WriteLine ("[{0}]", line);
		}
	}
}
