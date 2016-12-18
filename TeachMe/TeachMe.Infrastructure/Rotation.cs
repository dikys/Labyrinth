using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Infrastructure
{
    public enum Angles : int { Left = 180, Right = 0, Up = 90, Down = 270 }
    
    public struct Rotation
    {
        public Rotation (Angles angle)
        {
            Angle = angle;
        }

        public Angles Angle { get; }

        public Location GetForward ()
        {
            if (Angle == Angles.Right)
                return new Location(1, 0);
            else if (Angle == Angles.Up)
                return new Location(0, 1);
            else if (Angle == Angles.Left)
                return new Location(-1, 0);
            else
                return new Location(0, -1);
        }
        
        public static Angles ConvertToAngle(int angle)
        {
            if (0 <= angle && angle < 360)
                return (Angles)angle;

            if (angle % 360 == 0)
                return 0;

            if (angle < 0)
            {
                angle = (angle / 360 + 1)*360 + angle;
            }
            else if (360 < angle)
            {
                angle = angle - (angle / 360 + 1) * 360;
            }
            
            return (Angles)(angle);
        }
        
        #region value semantics

        public static explicit operator Rotation(int angle)
        {
            return new Rotation(ConvertToAngle(angle));
        }

        public static Rotation operator +(Rotation left, int angle)
        {
            return new Rotation(ConvertToAngle((int)left.Angle + angle));
        }

        public static double operator -(Rotation left, Rotation right)
        {
            var leftAngle = (int) left.Angle;
            var rightAngle = (int) right.Angle;

            var a1 = left.GetForward();
            var a2 = right.GetForward();

            var cosA = a1.X*a2.X + a1.Y*a2.Y;
            var sinA = a1.X*a2.Y - a1.Y*a2.X;

            var result = Math.Acos(cosA);

            if (sinA < 0)
                return -result;

            return result;

            /*if (leftAngle < rightAngle)
            {
                leftAngle += 360;

                return -(leftAngle - rightAngle);
            }

            return leftAngle - rightAngle;*/
        }

        private bool Equals(Rotation other)
        {
            return Angle == other.Angle;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            
            return Equals((Rotation)obj);
        }

        #endregion
    }
}
