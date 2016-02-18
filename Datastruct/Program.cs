using System;

namespace Datastruct
{

	class MainClass
	{
		public static void Main (string[] args)
		{
			int num = 1000000;
			StupidList<int> sl = new StupidList<int> (num);
			//for (int i=0; i<=num; i++) {
			//	sl.Add (i);
			//}
			DateTime startTime;
			startTime = DateTime.Now;
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (num/3);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (num/3);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (num/3);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (num-10);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (0);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			sl.Remove (0);
			Console.WriteLine (DateTime.Now - startTime);

			startTime = DateTime.Now;
			for( long i=0; i<100000000; i++);
			Console.WriteLine (DateTime.Now - startTime);

		}
	}
}
