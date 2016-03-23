using System;
using System.Collections;
using System.Collections.Generic;

namespace QuadTree
{
	public class Game
	{
		private List<GameObject> Objects;
		private Dictionary<string, GameObject> ObjectsByName;
		private bool PrintRequested;

		public int CurrentTick{ get; private set; }
		public bool Running{ get; private set; }

		public Game ()
		{
			Objects = new List<GameObject> ();
			ObjectsByName = new Dictionary<string, GameObject>(50);
			Running = false;
			PrintRequested = false;
		}

		public void AddObject(string name, int x1, int y1)
		{
			var obj = new GameObject (name, x1, y1);
			ObjectsByName [obj.Name] = obj;
			Objects.Add (obj);
		}

		public void MoveObject( string name, int x1, int y1 )
		{
			try{
				ObjectsByName [name].Move(x1, y1);
			} catch (KeyNotFoundException) {
				Console.WriteLine ("{0} not found", name);
			}
		}

		private void SortObjects()
		{
			for (var i=1; i<Objects.Count; i++) {
				var j = i;
				while (j > 0 && Objects[j].X1 < Objects[j-1].X1){
					var tmp = Objects [j];
					Objects [j] = Objects [j-1];
					Objects [j-1] = tmp;
					j--;
				}
			}
		}

		public List<GameObject[]> SweepAndPrune()
		{
			List<GameObject[]> result = new List<GameObject[]>();

			for (var i=0; i< Objects.Count-1; i++) {
				for (var j=i+1; j<Objects.Count; j++) {
					if (Objects [i].NextIntersects (Objects [j])) {
						result.Add (new GameObject[] { Objects[i], Objects[j] });
					} else {
						break;
					}
				}
			}
			return result;
		}

		public void PrintCollisions()
		{
			PrintRequested = true;
		}

		private void DoPrintCollisions()
		{
			foreach( var collision in SweepAndPrune()) {
				Console.WriteLine("({0}) {1} collides with {2}", CurrentTick, collision[0].Name, collision[1].Name);
			}
		}

		public void Start()
		{
			Running = true;
			CurrentTick = 1;
		}

		public void Tick()
		{
			SortObjects();
			if (PrintRequested) {
				DoPrintCollisions ();
				PrintRequested = false;
			}
			CurrentTick++;
		}

		public void End()
		{
			Running = false;
		}
	}
}

