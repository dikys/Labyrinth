using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Domain
{
    public class GameModel
    {
        public GameModel(Robot robot, Field field)
        {
            if (robot == null)
                throw new ArgumentNullException("robot");
            if (field == null)
                throw new ArgumentNullException("field");

            this.Robot = robot;
            this.Field = field;
        }
        
        public Robot Robot { get; private set; }
        public Field Field { get; private set; }
    }
}
