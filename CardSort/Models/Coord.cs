using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CardSort.Models
{
    public struct Coord : IEquatable<Coord>
    {
        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Coord other) =>
            this.X == other.X && this.Y == other.Y;

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return c1.X != c2.X || c1.Y != c2.Y;
        }
    }
}
