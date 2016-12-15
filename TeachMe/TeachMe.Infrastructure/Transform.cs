using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Infrastructure
{
    public struct Transform
    {
        public Transform (Location location = new Location(), Rotation rotation = new Rotation())
        {
            Location = location;
            Rotation = rotation;
        }

        public Location Location { get; }
        public Rotation Rotation { get; }
        public Location Forward => Rotation.GetForward();

        #region value semantics

        public Transform Rotate(int angle)
        {
            return new Transform(Location, Rotation + angle);
        }

        public static Transform operator +(Transform left, Location right)
        {
            return new Transform(left.Location + right, left.Rotation);
        }

        public static Transform operator -(Transform left, Location right)
        {
            return new Transform(left.Location - right, left.Rotation);
        }

        private bool Equals(Transform other)
        {
            return Location.Equals(other.Location) && Rotation.Equals(other.Rotation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals((Transform)obj);
        }

        #endregion
    }
}
