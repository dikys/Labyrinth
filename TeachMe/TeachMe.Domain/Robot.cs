using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Infrastructure;

namespace TeachMe.Domain
{
    public class Robot
    {
        public Robot(Location location)
        {
            this.Location = location;
        }

        public Location Location { get; private set; }
    }
}
