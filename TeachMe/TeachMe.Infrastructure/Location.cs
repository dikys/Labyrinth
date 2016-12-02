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
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        //public Location Rotate(Rotation rotation)
        //{
        //    return new Location(
        //        this.X * Math.Cos(rotation.Angle) - this.Y * Math.Sin(rotation.Angle),
        //        this.X * Math.Sin(rotation.Angle) - this.Y * Math.Cos(rotation.Angle));
        //}
        

        public static Location operator+ (Location left, Location rigth)
        {
            return new Location(left.X + rigth.X, left.Y + rigth.Y);
        }

        public static Location operator- (Location left, Location rigth)
        {
            return new Location(left.X - rigth.X, left.Y - rigth.Y);
        }
    }
}
