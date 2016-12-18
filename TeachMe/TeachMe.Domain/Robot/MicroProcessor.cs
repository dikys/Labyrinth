using System;
using System.Collections.Generic;

namespace TeachMe.Domain.Robot
{
    public class MicroProcessor
    {
        public MicroProcessor()
        {
            Commands = new List<Action>();
            CurrentCommandNumber = 0;
        }
        
        public List<Action> Commands { get; }
        public bool IsFinish => CurrentCommandNumber == Commands.Count;
        private int _currentCommandNumber = 0;
        public int CurrentCommandNumber
        {
            get { return _currentCommandNumber; }
            set
            {
                if (!(0 <= value && value <= Commands.Count))
                    throw new ArgumentOutOfRangeException("CurrentCommandNumber should be between 0 and " + (Commands.Count - 1));

                _currentCommandNumber = value;
            }
        }

        public void Reset()
        {
            CurrentCommandNumber = 0;
        }
        
        public void Run()
        {
            if (IsFinish)
                return;

            Commands.ForEach(command => command());
            CurrentCommandNumber = Commands.Count - 1;
        }

        public void RunNext()
        {
            if (IsFinish)
                return;

            Commands[CurrentCommandNumber++]();
        }
    }
}
