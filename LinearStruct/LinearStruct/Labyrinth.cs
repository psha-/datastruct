using System;

namespace LinearStruct
{
	public class LabyrinthPos
	{
		public LabyrinthPos(int x_, int y_)
		{
			x = x_;
			y = y_;
		}

		public LabyrinthPos(LabyrinthPos pos)
		{
			x = pos.x;
			y = pos.y;
		}

		public int x;
		public int y;
	}


	public class LabyrinthMap
	{
		public enum mapItem {
			// Free position
			o = 0,
			// Obstacle
			x = -1,
			// Player
			P = -2,
			// Unreachable
			U = -3
		};

		public mapItem [,] map { get; private set;}
		public int w { get; private set;}
		public int h {get; private set;}

		public LabyrinthMap(mapItem[,] m)
		{
			map = m;
			h = m.GetLength (0);
			w = m.GetLength (1);
		}

		public LabyrinthMap(LabyrinthMap m)
		{
			map = (mapItem[,])m.map.Clone();
			w = m.w;
			h = m.h;
		}

		public void MarkFreeAsU()
		{
			for (int i = 0; i < map.GetLength(0); i++) {
				for (int j = 0; j < map.GetLength(1); j++) {
					if (mapItem.o == map [i, j]) {
						map [i, j] = mapItem.U;
					}
				}
			}
		}

		public void Print()
		{
			for (int i = 0; i < map.GetLength(0); i++) {
				for (int j = 0; j < map.GetLength(1); j++) {
					Console.Write(string.Format("{0} ", map[i, j]).PadRight(3));
				}
				Console.Write(Environment.NewLine);
			}
			Console.Write(Environment.NewLine);
		}

		public mapItem this[LabyrinthPos pos]
		{
			get
			{
				return map[pos.y,pos.x];
			}
			set
			{
				map[pos.y,pos.x] = value;
			}
		}

	}


	public class Labyrinth
	{
		public LabyrinthMap map { get; private set;}
		private LabyrinthPos pos;


		public Labyrinth ( LabyrinthMap m, LabyrinthPos start )
		{
			if (LabyrinthMap.mapItem.x == m [start]) {
				throw new AccessViolationException ();
			}
			map = m;
			pos = start;
			map [start] = LabyrinthMap.mapItem.P;
		}

		public LabyrinthMap calcDistances()
		{
			var dstMap = new LabyrinthMap(map);
			Move (ref dstMap, pos, pos);
			return dstMap;
		}

		private void Move(ref LabyrinthMap dstMap, LabyrinthPos currentPos, LabyrinthPos prevPos)
		{
			if (LabyrinthMap.mapItem.o != dstMap [currentPos] && LabyrinthMap.mapItem.P != dstMap [currentPos]) {
				// Current position is not free
				return;
			}

			// TODO: Make sure the start position is treated as obsticle after the 1st run
			LabyrinthMap.mapItem dst;
			if (LabyrinthMap.mapItem.P == dstMap [prevPos]) {
				dst = LabyrinthMap.mapItem.o;
				dstMap [prevPos] = LabyrinthMap.mapItem.x;
			}
			else {
				dst = dstMap [prevPos]+1;

			}
			dstMap [currentPos] = dst;

			if (currentPos.x > 0) {
				Move (ref dstMap, new LabyrinthPos(currentPos.x-1, currentPos.y), currentPos);
			}

			if (currentPos.x < dstMap.w-1) {
				Move (ref dstMap, new LabyrinthPos(currentPos.x+1, currentPos.y), currentPos);
			}

			if (currentPos.y > 0) {
				Move (ref dstMap, new LabyrinthPos(currentPos.x, currentPos.y-1), currentPos);
			}

			if (currentPos.y < dstMap.h-1) {
				Move (ref dstMap, new LabyrinthPos(currentPos.x, currentPos.y+1), currentPos);
			}
		}


	}
}

