using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Infrastructure
{
    public struct Location
    {
        public Location(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        
        #region value semantics

        public static Location operator +(Location left, Location rigth)
        {
            return new Location(left.X + rigth.X, left.Y + rigth.Y);
        }

        public static Location operator -(Location left, Location rigth)
        {
            return new Location(left.X - rigth.X, left.Y - rigth.Y);
        }

        private bool Equals(Location other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Location)obj);
        }

        #endregion
    }
}
