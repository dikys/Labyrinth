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
            this.Location = location;
            this.Rotation = rotation;
        }

        public Location Location { get; private set; }
        public Rotation Rotation { get; private set; }
        public Location Forward
        {
            get
            {
                return this.Rotation.GetForward();
            }
        }

        public static Transform operator+ (Transform left, Location right)
        {
            return new Transform(left.Location + right, left.Rotation);
        }

        public static Transform operator- (Transform left, Location right)
        {
            return new Transform(left.Location - right, left.Rotation);
        }

        public static Transform operator+ (Transform left, Rotation right)
        {
            return new Transform(left.Location, left.Rotation + right);
        }

        public static Transform operator- (Transform left, Rotation right)
        {
            return new Transform(left.Location, left.Rotation - right);
        }
    }
}
