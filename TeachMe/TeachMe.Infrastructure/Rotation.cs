using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Infrastructure
{
    public enum Angles : int { Left = -90, Right = 90, Up = 180, Down = -180 }

    public struct Rotation
    {
        public Rotation (Angles angle)
        {
            this.Angle = angle;
        }

        public Angles Angle
        {
            get
            {
                return this.Angle;
            }
            private set
            {
                //if (!(0 <= value && value <= 360))
                //    throw new ArgumentOutOfRangeException("Angle should be between 0 and 360");

                this.Angle = value;
            }
        }

        public Location GetForward ()
        {
            if (this.Angle == Angles.Right)
                return new Location(1, 0);
            else if (this.Angle == Angles.Up)
                return new Location(0, 1);
            else if (this.Angle == Angles.Left)
                return new Location(-1, 0);
            else
                return new Location(0, -1);
        }

        public static Rotation operator+ (Rotation left, Rotation right)
        {
            return new Rotation((Angles)((int)left.Angle + (int)right.Angle));
        }

        public static Rotation operator- (Rotation left, Rotation right)
        {
            return new Rotation((Angles)((int)left.Angle - (int)right.Angle));
        }

        //public static int Trim (int value)
        //{
        //    return Math.Min(Math.Max(value, 360), 0);
        //}

        //public static Rotation operator+ (Rotation left, Rotation right)
        //{
        //    return new Rotation(left.Angle + right.Angle);
        //}
    }
}
