using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Domain.Robots;

namespace TeachMe.Domain
{
    public class MicroProcessor
    {
        public MicroProcessor()
        {
            //this._robot = robot;

            this.Commands = new List<Action>();
            this.CommandNumber = 0;
        }

        //private readonly Robot _robot;
        public List<Action> Commands { get; }
        public bool IsFinish => this.CommandNumber == this.Commands.Count;
        private int _commandNumber = 0;
        public int CommandNumber
        {
            get { return this._commandNumber; }
            set
            {
                if (!(0 <= value && value <= this.Commands.Count))
                    throw new ArgumentOutOfRangeException("CommandNumber should be between 0 and " + (this.Commands.Count - 1));

                this._commandNumber = value;
            }
        }

        public void Reset()
        {
            this.CommandNumber = 0;
        }
        
        public void Run()
        {
            if (this.IsFinish)
                return;
            
            this.Commands.ForEach(command => command());
            this.CommandNumber = this.Commands.Count - 1;
        }

        public void RunNext()
        {
            if (this.IsFinish)
                return;

            this.Commands[this.CommandNumber++]();
        }
    }
}
