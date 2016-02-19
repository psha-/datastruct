using System;
using System.Collections.Generic;

namespace StupidList
{

	class MainClass
	{
		public static void Main (string[] args)
		{
			var list = new StupidList<int>(100);
			list.Add (4);
		}
	}
}
