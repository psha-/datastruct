using System;
using System.Collections;
using System.Collections.Generic;

namespace StacksQueues
{
	public class ANumberSequence: IEnumerable
	{
		protected int First;
		protected int Count;
		protected int Index;
		protected Queue<int> sequence;
		protected int Op;

		public ANumberSequence (int f, int c)
		{
			First = f;
			Count = c;

			sequence = new Queue<int> (Count);
			sequence.Enqueue (First);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator ();
		}

		public IEnumerator<int> GetEnumerator()
		{

			yield return First;
			int s=0;
			for (var i=0; i<Count; i++) {
				switch( Op ) {
				case 0:
					s = sequence.Peek () + 1;
					sequence.Enqueue (s);
					break;
				case 1:
					s = 2 * sequence.Peek () + 1;
					sequence.Enqueue (s);
					break;
				case 2:
					s = sequence.Dequeue () + 2;
					sequence.Enqueue (s);
					break;
				}
				Op = (Op + 1) % 3;
				yield return s;
			}
		}
	}
}

