using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;
namespace BiDictionary
{
	public class BiDictionary<K1, K2, T>
		where K1: IComparable
		where K2: IComparable
	{
		private MultiDictionary<Tuple<K1,K2>, T> ElementsByK1K2;
		private Dictionary<K1, MultiDictionary<K2, T>> ElementsByK1;
		private Dictionary<K2, MultiDictionary<K1, T>> ElementsByK2;

		public BiDictionary ()
		{
			ElementsByK1K2 = new MultiDictionary<Tuple<K1,K2>, T> (true);
			ElementsByK1 = new Dictionary<K1, MultiDictionary<K2, T>> (100);
			ElementsByK2 = new Dictionary<K2, MultiDictionary<K1, T>> (100);
		}

		public void Add(K1 key1, K2 key2, T value)
		{
			if (!ElementsByK1.ContainsKey(key1)) {
				ElementsByK1[key1] = new MultiDictionary<K2, T>(true);
			}
			if (!ElementsByK2.ContainsKey (key2)) {
				ElementsByK2[key2] = new MultiDictionary<K1, T>(true);
			}
			ElementsByK1 [key1] [key2].Add (value);
			ElementsByK2 [key2] [key1].Add (value);
			ElementsByK1K2 [new Tuple<K1,K2>(key1, key2)].Add(value);
		}

		public bool Remove(K1 key1, K2 key2)
		{
			ElementsByK1 [key1].Remove (key2);
			if (0 == ElementsByK1 [key1].Count) {
				ElementsByK1.Remove (key1);
			}
			ElementsByK2 [key2].Remove (key1);
			if (0 == ElementsByK2 [key2].Count) {
				ElementsByK2.Remove (key2);
			}
			return ElementsByK1K2.Remove(new Tuple<K1,K2>(key1, key2));
		}

		public IEnumerable<T> Find(K1 key1, K2 key2)
		{
			var result = ElementsByK1K2[new Tuple<K1,K2>(key1, key2)];
			return result;
		}

		public IEnumerable<T> FindByKey1(K1 key)
		{
			return ElementsByK1.ContainsKey (key) ? ElementsByK1 [key].Values : Enumerable.Empty<T> ();
		}

		public IEnumerable<T> FindByKey2(K2 key)
		{
			return ElementsByK2.ContainsKey (key) ? ElementsByK2 [key].Values : Enumerable.Empty<T> ();
		}
	}
}

