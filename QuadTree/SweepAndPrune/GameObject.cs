using System;

namespace QuadTree
{
	public class GameObject
	{
		public string Name{ get; private set;}
		public int X1{ get; private set;}
		public int Y1{ get; private set;}
		public int Width{ get; private set;}
		public int Height{ get; private set;}


		public GameObject (string name, int x1, int y1)
		{
			Name = name;
			X1 = x1;
			Y1 = y1;
			Width = 10;
			Height = 10;
		}

		public void Move(int x1, int y1)
		{
			X1 = x1;
			Y1 = y1;
		}

		public bool NextIntersects(GameObject next)
		{
			return next.X1 <= X1+Width && next.Y1 <= Y1+Height && next.Y1+Height > Y1;

		}
	}
}

