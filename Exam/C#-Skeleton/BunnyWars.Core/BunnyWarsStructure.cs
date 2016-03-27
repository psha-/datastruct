namespace BunnyWars.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        LinkedList<int> RoomsLinked = new LinkedList<int>();
        //private OrderMultiDictionary<string, Bunny> BunnySuffixes = new OrderMultiDictionary<string, Bunny>(new BunnyComparer());

        private SortedList<int, SortedSet<Bunny>> Rooms = new SortedList<int, SortedSet<Bunny>>();
        private SortedDictionary<string, int> RoomIdsByBunnyName = new SortedDictionary<string, int>();
        private SortedDictionary<string, Bunny> BunniesByName = new SortedDictionary<string, Bunny>();
        private SortedDictionary<int, SortedSet<Bunny>> BunniesByTeam = new SortedDictionary<int, SortedSet<Bunny>>();
        private Dictionary<int, Dictionary<int, SortedSet<Bunny>>> BunniesByRoomTeam = new Dictionary<int, Dictionary<int, SortedSet<Bunny>>>();

        public int BunnyCount {
            get {
                return BunniesByName.Count;
            }
        }

        public int RoomCount {
            get {
                return Rooms.Count;
            }
        }

        public void AddRoom(int roomId)
        {
            Rooms.Add(roomId, new SortedSet<Bunny>());
            BunniesByRoomTeam[roomId] = new Dictionary<int, SortedSet<Bunny>>();
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if(!Rooms.ContainsKey(roomId)) {
                throw new ArgumentException("Room " + roomId + " does not exist.");
            }
            if (!BunniesByTeam.ContainsKey(team))
            {
                BunniesByTeam[team] = new SortedSet<Bunny>();
            }
            if(!BunniesByRoomTeam.ContainsKey(roomId))
            {
                BunniesByRoomTeam[roomId] = new Dictionary<int, SortedSet<Bunny>>();
            }
            if (!BunniesByRoomTeam[roomId].ContainsKey(team))
            {
                BunniesByRoomTeam[roomId][team] = new SortedSet<Bunny>();
            }

            var bunny = new Bunny(name, team, roomId);

            //BunnySuffixes[name] = bunny;
            Rooms[roomId].Add(bunny);
            BunniesByName.Add(name, bunny);
            RoomIdsByBunnyName.Add(name, roomId);
            BunniesByTeam[team].Add(bunny);
            BunniesByRoomTeam[roomId][team].Add(bunny);
        }

        public void Remove(int roomId)
        {
            if(!Rooms.ContainsKey(roomId))
            {
                throw new ArgumentException("Room " + roomId + " does not exist.");
            }
            var bunnies = Rooms[roomId];
            Rooms.Remove(roomId);
            foreach (var bunny in bunnies)
            {
                //BunnySuffixes.Remove(bunny.Name);
                BunniesByName.Remove(bunny.Name);
                BunniesByTeam.Remove(bunny.Team);
                BunniesByRoomTeam.Remove(roomId);
            }
        }

        public void Next(string bunnyName)
        {
            if ( !BunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny " + bunnyName + " does not exist");
            }

            var bunny = BunniesByName[bunnyName];
            var roomIndex = Rooms.IndexOfKey(bunny.RoomId);
            //var roomIndex = Rooms.ElementAt(bunny.RoomId);

            //var nextRoomIndex = (roomIndex + 1) % Rooms.Count;
            var nextRoomIndex = roomIndex + 1;
            if (nextRoomIndex == RoomCount )
            {
                nextRoomIndex = 0;
            }
            if (roomIndex != nextRoomIndex) {
                var nextRoom = Rooms.ElementAt(nextRoomIndex);
                nextRoom.Value.Add(bunny);
                Rooms.ElementAt(roomIndex).Value.Remove(bunny);
                //TODO check for empty
                //var currentRoom = Rooms
                //      if( !currentRoom.MoveNext() )
                //     {
                //    currentRoom = Rooms.GetEnumerator().Current.Value;
                //   }
                //   currentRoom.
                bunny.RoomId = nextRoom.Key;
            }
            
        }

        public void Previous(string bunnyName)
        {
            if (!BunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny " + bunnyName + " does not exist");
            }

            var bunny = BunniesByName[bunnyName];
            var roomIndex = Rooms.IndexOfKey(bunny.RoomId);
            //var roomIndex = Rooms.ElementAt(bunny.RoomId);

            //var nextRoomIndex = (roomIndex + 1) % Rooms.Count;
            var nextRoomIndex = roomIndex - 1;
            if (nextRoomIndex < 0)
            {
                nextRoomIndex = RoomCount - 1;
            }
            if (roomIndex != nextRoomIndex)
            {
                var nextRoom = Rooms.ElementAt(nextRoomIndex);
                nextRoom.Value.Add(bunny);
                Rooms.ElementAt(roomIndex).Value.Remove(bunny);
                //TODO check for empty
                //var currentRoom = Rooms
                //      if( !currentRoom.MoveNext() )
                //     {
                //    currentRoom = Rooms.GetEnumerator().Current.Value;
                //   }
                //   currentRoom.
                bunny.RoomId = nextRoom.Key;
            }

        }

        public void Detonate(string bunnyName)
        {
            if( !BunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny " + bunnyName + " does not exist");
            }
            var bunny = BunniesByName[bunnyName];
            var roomId = bunny.RoomId;
            var otherTeams = BunniesByRoomTeam[roomId].Keys.Except(new int[] { bunny.Team } ).ToArray() ;
            if( 0 == otherTeams.Length )
            {
                return;
            }
            foreach (var teamId in otherTeams)
            {
                //if( 0 == BunniesByRoomTeam[roomId][teamId].Count )
                //{
                //    continue;
                //}
                //var otherBunny = BunniesByRoomTeam[roomId][teamId].GetEnumerator();
                //do
                //{
                //if( null == otherBunny.Current)
                //{
                //    continue;
                //}
                //HitBunny(bunny, otherBunny.Current);
                //}
                //while (otherBunny.MoveNext());
                var otherBunnies = BunniesByRoomTeam[roomId][teamId].ToArray();
                foreach (var otherBunny in otherBunnies)
                {
                    HitBunny(bunny, otherBunny);
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if( !BunniesByTeam.ContainsKey(team))
            {
                throw new IndexOutOfRangeException("Team not found");
            }
            return BunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            throw new NotImplementedException();
            //return BunnySuffixes[suffix];
        }

        private void HitBunny(Bunny hitter, Bunny bunny)
        {
            bunny.Health -= 30;
            if( bunny.Health <= 0 )
            {
                RemoveBunny(bunny);
                hitter.Score++;
            }
        }
        private void RemoveBunny(Bunny bunny)
        {
            BunniesByName.Remove(bunny.Name);
            //BunnySuffixes.Remove(bunny.Name);
            BunniesByTeam[bunny.Team].Remove(bunny);
            if( 0 == BunniesByTeam.Count )
            {
                BunniesByTeam.Remove(bunny.Team);
            }
            Rooms[bunny.RoomId].Remove(bunny);
            BunniesByRoomTeam[bunny.RoomId][bunny.Team].Remove(bunny);
            if( 0 == BunniesByRoomTeam[bunny.RoomId][bunny.Team].Count )
            {
                BunniesByRoomTeam[bunny.RoomId].Remove(bunny.Team);
            }
        }
    }

    class BunnyComparer : Comparer<string>
    {
        public override int Compare(string current, string other)
        {
            var currentName = current;
            var otherName = other;

            var from = Math.Max(currentName.Length, otherName.Length);
            var to = from - Math.Min(currentName.Length, otherName.Length);

            for (var i = from - 1; i >= to; i--)
            {
                if (currentName[i] > otherName[i])
                {
                    return 1;
                }
                else if (currentName[i] < otherName[i])
                {
                    return -1;
                }
            }
            return currentName.Length - otherName.Length;
        }
    }
}
