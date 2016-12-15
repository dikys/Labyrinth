using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Domain.Robot;

namespace TeachMe.Domain
{
    public class GameModel
    {
        public GameModel(Robot.MobileRobot robot, Field field)
        {
            if (robot == null)
                throw new ArgumentNullException("robot");
            if (field == null)
                throw new ArgumentNullException("field");

            Robot = robot;
            Field = field;
        }
        
        public MobileRobot Robot { get; private set; }
        public Field Field { get; private set; }
    }
}
