using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Infrastructure;

/*
 * Можно сделать потом разные виды микропроцессоров
*/

namespace TeachMe.Domain
{
    public class Robot
    {
        public class MicroProcessor
        {
            public MicroProcessor (Robot robot)
            {
                this.robot = robot;
            }

            private Robot robot;
            public List<Action> Commands { get; private set; } // Может стоит IReadOnly на get кинуть

            public void Run ()
            {
                this.Commands.ForEach(command => command());
            }

            public void Forward ()
            {
                this.robot.Transform = this.robot.Transform + this.robot.Transform.Forward;
            }

            public void Backward ()
            {
                this.robot.Transform = this.robot.Transform - this.robot.Transform.Forward;
            }

            public void Rigthward ()
            {
                this.robot.Transform = this.robot.Transform + new Rotation(Angles.Right);
            }
        }

        public Robot (Transform transform)
        {
            this.Processor = new MicroProcessor(this);

            this.Transform = transform;
        }

        public Transform Transform { get; private set; }
        public MicroProcessor Processor { get; private set; }
    }
}
