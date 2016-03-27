namespace BunnyWars.Core
{
    using System;

    public class Bunny: IComparable<Bunny>
    {
        public Bunny(string name, int team, int roomId)
        {
            if( team < 0 || team > 4 )
            {
                throw new IndexOutOfRangeException("Bunny team should be in [0-4].");
            }
            Name = name;
            Team = team;
            RoomId = roomId;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; } = 100;

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(Bunny other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
