using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeachMe.Infrastructure;

/*
 * Можно сделать потом разные виды микропроцессоров
*/

namespace TeachMe.Domain.Robot
{
    public class MobileRobot
    {
        public Transform Transform { get; private set; }

        private readonly MicroProcessor _processor;
        
        public List<Command> AvailableCommands { get; private set; }
        public bool IsProgrammEnd => _processor.IsFinish;
        public int CurrentCommandNumber => _processor.CurrentCommandNumber;

        //public event Action<string> CantDoCommand;
        
        public MobileRobot (Transform transform)
        {
            _processor = new MicroProcessor();
            Transform = transform;

            DownloadCommands();
        }

        public void RunProgramm()
        {
            while (IsProgrammEnd)
                RunNextCommand();
        }

        public void RunNextCommand()
        {
            _processor.ExecuteNextCommand();
        }

        public void RebootProgramm()
        {
            _processor.Reboot();
        }

        public void ClearProgramm()
        {
            _processor.Clear();
        }

        public void AddCommands(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                AddCommand(command);
            }
        }

        public void AddCommands(IEnumerable<int> commandIndexes)
        {
            foreach (var commandIndex in commandIndexes)
            {
                AddCommand(commandIndex);
            }
        }

        public void AddCommand(Command command)
        {
            _processor.Commands.Add(command.Method);
        }

        public void AddCommand(int commandIndex)
        {
            if (commandIndex < 0 || AvailableCommands.Count <= commandIndex)
                throw new ArgumentException("commandIndex should be between 0 and " + AvailableCommands.Count);

            _processor.Commands.Add(AvailableCommands[commandIndex].Method);
        }

        public bool CheckCanDoCommand()
        {
            var can = false;

            var message = "Я не могу пойти туда";

            return can;
        }

        private void DownloadCommands()
        {
            AvailableCommands = new List<Command>();

            this.AvailableCommands
                .AddRange(GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(methodInfo => methodInfo.GetCustomAttribute(typeof (CommandInfoAttribute), true) != null)
                    .Select(methodInfo => new Command(methodInfo, this)));
        }
        
        #region Robot commands
        /*
         * По названию метода происходит загрузка нужной иконки и нужной анимации
         * 
         * 
        */

        [CommandInfo(Name = "Вперед", Description = "Двигаться на 1 клетку вперед")]
        public void Forward()
        {
            Transform = Transform + Transform.Forward;
        }
         
        [CommandInfo(Name = "Назад", Description = "Двигаться на 1 клетку назад")]
        public void Backward()
        {
            Transform = Transform - Transform.Forward;
        }
        
        [CommandInfo(Name = "Налево", Description = "Повернуться на 90 по часовой")]
        public void Leftward()
        {
            Transform = Transform.Rotate(90);
        }

        [CommandInfo(Name = "Направо", Description = "Повернуться на 90 против часовой")]
        public void Rightward()
        {
            Transform = Transform.Rotate(-90);
        }
        #endregion
    }
}
