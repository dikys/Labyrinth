using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Domain
{
    public class MicroProcessor
    {
        public MicroProcessor()
        {
            //this._robot = Robot;

            Commands = new List<Action>();
            CommandNumber = 0;
        }

        //private readonly Robot _robot;
        public List<Action> Commands { get; }
        public bool IsFinish => CommandNumber == Commands.Count;
        private int _commandNumber = 0;
        public int CommandNumber
        {
            get { return _commandNumber; }
            set
            {
                if (!(0 <= value && value <= Commands.Count))
                    throw new ArgumentOutOfRangeException("CommandNumber should be between 0 and " + (Commands.Count - 1));

                _commandNumber = value;
            }
        }

        public void Reset()
        {
            CommandNumber = 0;
        }
        
        public void Run()
        {
            if (IsFinish)
                return;
            
            Commands.ForEach(command => command());
            CommandNumber = Commands.Count - 1;
        }

        public void RunNext()
        {
            if (IsFinish)
                return;

            Commands[CommandNumber++]();
        }
    }
}
