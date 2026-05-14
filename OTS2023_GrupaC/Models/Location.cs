

using System;

namespace OTS2026_GrupaD.Models
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Location()
        {

        }

        public Location(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        override
        public bool Equals(Object obj)
        {
            return ((Location)obj).X == X && ((Location)obj).Y == Y && ((Location)obj).Z == Z;
        }
    }
}
