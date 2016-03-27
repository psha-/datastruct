using System;
using System.Collections.Generic;

namespace BunnyWars.Core
{
    class BunnyComparer : Comparer<string>
    {
        public override int Compare(string current, string other)
        {
            for (var i = 0; i < Math.Min(current.Length, other.Length); i++)
            {
                char currentChar = current[current.Length - i - 1];
                char otherChar = other[other.Length - i - 1];
                if ( currentChar < otherChar )
                {
                    return -1;
                }
                else if (currentChar > otherChar)
                {
                    return 1;
                }
            }
            return -other.Length + current.Length;
        }
    }
}
