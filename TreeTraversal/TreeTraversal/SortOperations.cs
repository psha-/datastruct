using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeTraversal
{
	public class SortOperations
	{
		public class Combination
		{
			public int[] Numbers{ get; private set; }
			public int Length{ get; private set; }
			public int Step{ get; private set; }

			public Combination(int[] numbers, int step=0)
			{
				Numbers = numbers;
				Length = numbers.Length;
				Step = step;
			}

			public int this[int i]
			{
				get { return Numbers[i]; }
				set { Numbers[i] = value; }
			}

			public Combination GetSubCombination(int i, int step)
			{
				return new Combination(Numbers.Where ((source, index) => index != i).ToArray (), step);
			}
		}

		private Combination StartNumbers;
		private int ReorderCount;
		private Dictionary<string, bool> GeneratedPermutations;

		public SortOperations ( int[] numbers, int reorder )
		{
			StartNumbers = new Combination(numbers);
			ReorderCount = reorder;
			GeneratedPermutations = new Dictionary<string, bool> ();
		}

		private bool PermutationExists(int[] permutation)
		{
			var perm = string.Join ("", permutation);
			if (GeneratedPermutations.ContainsKey (perm)) {
				return true;
			}
			GeneratedPermutations [perm] = true;
			return false;
		}

		private List<Combination> GeneratePermutations(Combination numbers) {
			List<Combination> permutations = new List<Combination>();
			if (1 == numbers.Length) {
				permutations.Add (numbers);
				return permutations;
			}

			for (var i=0; i<numbers.Length; i++) {
				List<Combination> subPerm = GeneratePermutations (numbers.GetSubCombination(i, numbers.Step+1));
				foreach (var sub in subPerm) {
					var newNumbers = new int[numbers.Length];
					newNumbers [0] = numbers [i];
					var length = sub.Length;
					Array.Copy (sub.Numbers, 0, newNumbers, 1, length);
					permutations.Add (new Combination(newNumbers, sub.Step));
				}
			}
			return permutations;
		}

		private bool IsSorted(int[] numbers)
		{
			for(var i=1; i<numbers.Length; i++) {
				if( numbers[i] < numbers[i-1] ){
					return false;
				}
			}
			return true;
		}

		public int GetSortOperations()
		{
			Queue<Combination> combinations = new Queue<Combination> ();
			combinations.Enqueue (StartNumbers);
			int combinationsCount = 0;
			while (combinations.Count > 0) {
				Combination current = combinations.Dequeue ();
				if (IsSorted (current.Numbers)) {
					if( 0 == current.Step % ReorderCount ) {
						return current.Step / ReorderCount;
					}
				}
				combinationsCount++;
				var next = new Combination(current.Numbers, current.Step+1);
				List<Combination> permutations = GeneratePermutations( next );
				foreach (var perm in permutations) {
					if (!PermutationExists (perm.Numbers)) {
						combinations.Enqueue (perm);
					}
				}
			}
			return -1;
		}
	}
}

