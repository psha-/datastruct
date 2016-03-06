using System;
using System.Collections.Generic;

namespace TreeTraversal
{
	public class Horse
	{
		public class Position
		{
			public int Row{ get; private set;}
			public int Col{ get; private set;}
			public int Value{ get; private set;}
			public Position(int row, int col, int val) {
				Row = row;
				Col = col;
				Value = val;
			}
		}

		public int[,] Board{ get; private set;}
		public Horse (int rows, int cols)
		{
			Board = new int[rows, cols];
		}

		public void TraverseBoard( Position pos )
		{
			var movements = new Queue<Position> ();
			movements.Enqueue (pos);
			while (movements.Count > 0) {
				pos = movements.Dequeue ();
				if (!CanMove (pos)) {
					continue;
				}
				Board [pos.Row, pos.Col] = pos.Value;

				movements.Enqueue (new Position (pos.Row - 2, pos.Col - 1, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row - 2, pos.Col + 1, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row + 2, pos.Col - 1, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row + 2, pos.Col + 1, pos.Value + 1));

				movements.Enqueue (new Position (pos.Row - 1, pos.Col - 2, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row - 1, pos.Col + 2, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row + 1, pos.Col - 2, pos.Value + 1));
				movements.Enqueue (new Position (pos.Row + 1, pos.Col + 2, pos.Value + 1));
			}
		}

		public bool CanMove(Position pos)
		{
			bool onBoard = ( pos.Row >= 0 && pos.Row < Board.GetLength(0) && pos.Col >= 0 && pos.Col < Board.GetLength(1));
			if (onBoard) {
				bool free = (0 == Board [pos.Row, pos.Col]);
				return free;
			}
			return false;
		}
	}
}

