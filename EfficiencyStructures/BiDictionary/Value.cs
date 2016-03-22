using System;

namespace BiDictionary
{
	public class Value<K1, K2, T>
	{
		public K1 Key1{ get; private set; }
		public K2 Key2{ get; private set; }
		public T Val{ get; private set; }

		public Value (K1 key1, T val)
		{
			Key1 = key1;
			Val = val;
		}
		public Value (K2 key2, T val)
		{
			Key2 = key2;
			Val = val;
		}
	}
}

