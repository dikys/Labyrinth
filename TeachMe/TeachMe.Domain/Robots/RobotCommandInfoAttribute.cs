using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Domain.Robots
{
    public class RobotCommandInfoAttribute : Attribute
    {
        public string Name;
        public string Description;
    }
}
