using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
	public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
	{
		public const int InitialCapacity = 16;
		public const float LoadFactor = 0.75f;

		public int Count { get; private set; }
		private LinkedList<KeyValue<TKey, TValue>>[] Slots;

		public int Capacity
		{
			get
			{
				return Slots.Length;
			}
		}

		public HashTable(int capacity = InitialCapacity)
		{
			Slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
			Count = 0;
		}

		public void Add(TKey key, TValue value)
		{
			GrowIfNeeded();
			var slotIndex = FindSlotIndex(key);
			if ( null == Slots[slotIndex])
			{
				Slots[slotIndex] = new LinkedList<KeyValue<TKey, TValue>>();
			}
			foreach( var slot in Slots[slotIndex] )
			{
				if( slot.Key.Equals(key))
				{
					throw new ArgumentException("Key already exists: " + key);
				}
			}
			var pair = new KeyValue<TKey, TValue>(key, value);
			Slots[slotIndex].AddLast(pair);
			Count++;
		}

		private int FindSlotIndex( TKey key )
		{
			return Math.Abs( key.GetHashCode() ) % Capacity;
		}

		private void GrowIfNeeded()
		{
			if( (float)(Count+1)/Capacity > LoadFactor)
			{
				Grow();
			}
		}

		private void Grow()
		{
			var newSlots = new HashTable<TKey, TValue> (Capacity * 2);
			foreach ( var pair in this )
			{
				newSlots.Add(pair.Key, pair.Value);
			}

			Slots = newSlots.Slots;
			Count = newSlots.Count;
		}

		public bool AddOrReplace(TKey key, TValue value)
		{
			try
			{
				Add(key, value);
				return true;
			} catch(ArgumentException)
			{
				var pair = Find(key);
				pair.Value = value;
			}
			return false;
		}

		public TValue Get(TKey key)
		{
			var pair = Find(key);
			if (null == pair)
			{
				throw new KeyNotFoundException();
			}
			return Find(key).Value;
		}

		public TValue this[TKey key]
		{
			get
			{
				return Get(key);
			}
			set
			{
				AddOrReplace(key, value);
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var pair = Find(key);
			if( null == pair )
			{
				value = default(TValue);
				return false;
			}
			value = pair.Value;
			return true;
		}

		public KeyValue<TKey, TValue> Find(TKey key)
		{
			var slotIndex = FindSlotIndex(key);
			if (null != Slots[slotIndex])
			{
				foreach (var slot in Slots[slotIndex])
				{
					if (slot.Key.Equals(key))
					{
						return slot;
					}
				}
			}
			return null;

		}

		public bool ContainsKey(TKey key)
		{
			return null != Find(key);
		}

		public bool Remove(TKey key)
		{
			var slotIndex = FindSlotIndex(key);
			if (null != Slots[slotIndex])
			{
				var slot = Slots[slotIndex];
				var element = slot.First;
				while ( null != element  )
				{
					if (element.Value.Key.Equals(key))
					{
						slot.Remove(element);
						Count--;
						return true;
					}
					element = element.Next;
				}
			}
			return false;
		}

		public void Clear()
		{
			Slots = new LinkedList<KeyValue<TKey, TValue>>[Capacity];
			Count = 0;
		}

		public IEnumerable<TKey> Keys
		{
			get
			{
				return this.Select(element=>element.Key );
			}
		}

		public IEnumerable<TValue> Values
		{
			get
			{
				return this.Select(element => element.Value);
			}
		}

		public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
		{
			foreach (var slot in Slots)
			{
				if (null == slot)
				{
					continue;
				}
				foreach (var pair in slot)
				{
					yield return pair;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

