using System;

namespace TreeStruct
{
	public class File
	{
		public string Name{ get; private set;}
		public long Size{ get; private set;}

		public File (string name, long size)
		{
			Name = name;
			Size = size;
		}
	}

}

