namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private SortedList<int, SortedSet<Bunny>> Rooms = new SortedList<int, SortedSet<Bunny>>();
        private OrderedDictionary<string, Bunny> BunniesByName = new OrderedDictionary<string, Bunny>(new BunnyComparer());
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
            if(BunniesByName.ContainsKey(name))
            {
                throw new ArgumentException("Bunny " + name + " already exist.");
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

            Rooms[roomId].Add(bunny);
            BunniesByName[name] = bunny;
            BunniesByTeam[team].Add(bunny);
            BunniesByRoomTeam[roomId][team].Add(bunny);
        }

        public void Remove(int roomId)
        {
            if(!Rooms.ContainsKey(roomId))
            {
                throw new ArgumentException("Room " + roomId + " does not exist.");
            }
            var bunnies = Rooms[roomId].ToArray();
            foreach (var bunny in bunnies)
            {
                RemoveBunny(bunny);
            }
            Rooms.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if ( !BunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny " + bunnyName + " does not exist");
            }

            var bunny = BunniesByName[bunnyName];
            var roomIndex = Rooms.IndexOfKey(bunny.RoomId);
            var nextRoomIndex = (roomIndex + 1) % RoomCount;
            if (roomIndex != nextRoomIndex) {
                var nextRoomId = Rooms.Keys[nextRoomIndex];
                RemoveBunny(bunny);
                AddBunny(bunny.Name, bunny.Team, nextRoomId);
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
            var prevRoomIndex = (roomIndex - 1) % RoomCount;
            if( prevRoomIndex < 0 )
            {
                prevRoomIndex += RoomCount;
            }
            if (roomIndex != prevRoomIndex)
            {
                var nextRoomId = Rooms.Keys[prevRoomIndex];
                RemoveBunny(bunny);
                AddBunny(bunny.Name, bunny.Team, nextRoomId);
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
            var bunniesToRemove = new LinkedList<Bunny>();

            foreach (var teamId in otherTeams)
            {
                foreach ( var otherBunny in BunniesByRoomTeam[roomId][teamId])
                {
                    if( HitBunny(bunny, otherBunny))
                    {
                        bunniesToRemove.AddLast(otherBunny);
                    }
                }
            }
            while(bunniesToRemove.Count > 0)
            {
                RemoveBunny(bunniesToRemove.Last.Value);
                bunniesToRemove.RemoveLast();
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if( !BunniesByTeam.ContainsKey(team))
            {
                throw new IndexOutOfRangeException("Team " + team + " not found");
            }
            return BunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            if ("" == suffix)
            {
                return BunniesByName.Values;
            }
            return BunniesByName.Range(suffix, true, char.MaxValue + suffix, false).Values;
            }

        private bool HitBunny(Bunny hitter, Bunny bunny)
        {
            bunny.Health -= 30;
            if( bunny.Health <= 0 )
            {
                hitter.Score++;
                return true;
            }
            return false;
        }
        private void RemoveBunny(Bunny bunny)
        {
            BunniesByName.Remove(bunny.Name);
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

    
}
