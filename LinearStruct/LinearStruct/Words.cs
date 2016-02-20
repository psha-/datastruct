using System;
using System.Collections.Generic;
using System.Text;

namespace LinearStruct
{
	public class Words
	{
		private List<string> arr;
		private int Length;

		public Words (string input)
		{
			Length = input.Length;
			if (0 == Length) {
				arr = new List<string> ();
			} else {
				var words = input.Split ();
				arr = new List<string> (words.Length);

				foreach (var word in words) {
					arr.Add (word);
				}
			}
		}

		public string Sort()
		{
			var sortedArr = new List<string> (arr);
			sortedArr.Sort ();
			var result = new StringBuilder (Length);
			foreach (var item in sortedArr) {
				result.Append (item).Append(" ");
			}
			return result.ToString();
		}
	}
}

