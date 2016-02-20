using System;
using System.Collections.Generic;

namespace LinearStruct
{
	public class Integers
	{
		private List<int> arr;

		public Integers (String input)
		{
			if (0 == input.Length) {
				arr = new List<int> ();
			} else {
				var numbers = input.Split ();
				arr = new List<int> (numbers.Length);

				foreach (var number in numbers) {
					arr.Add (Int32.Parse (number));
				}
			}
		}

		public double Avg()
		{
			long sum=0;
			foreach( int item in arr ) {
				sum += item;
			}
			return 0 == arr.Count ? 0 : (double)sum/arr.Count;
		}

		public long Sum()
		{
			long sum=0;
			foreach( int item in arr ) {
				sum += item;
			}
			return sum;
		}

		public List<int> LongestSeq()
		{
			if (0 == arr.Count) {
				return new List<int>(0);
			}

			int seq_start = 0;
			int number = arr [0];
			int seq_length = 1;
			int seq_number = number;

			int i = 1;
			// Last element is not checked
			for (; i<arr.Count; i++) {
				if(arr [i] != number ) {
					if (i - seq_start > seq_length) {
						// Last sequence is longest
						seq_length = i - seq_start;
						seq_number = number;
					}
					seq_start = i;
					number = arr [i];
				}
			}

			if (i - seq_start > seq_length) {
				// Last sequence is longest
				seq_length = i - seq_start;
				seq_number = number;
			}

			List<int> seq = new List<int> (seq_length);
			for(i = 0; i < seq_length; i++)
			{
				seq.Add(seq_number);
			}
			return seq;
		}
	}
}

