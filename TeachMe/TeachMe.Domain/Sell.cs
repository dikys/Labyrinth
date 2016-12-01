using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Infrastructure;

namespace TeachMe.Domain
{
    public struct Sell
    {
        public Sell(Location location)
        {
            this.Location = location;
        }

        public Location Location { get; private set; }
    }
}
