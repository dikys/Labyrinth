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
        public MobileRobot()
        {
            Commands = new List<Command>();

            InitilizateCommands();
        }

        public MobileRobot (Transform transform) : this()
        {
            Processor = new MicroProcessor();
            Transform = transform;
        }

        public Transform Transform { get; private set; }
        public MicroProcessor Processor { get; }
        public List<Command> Commands { get; }

        public void InitilizateCommands()
        {
            this.Commands
                .AddRange(GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(methodInfo => methodInfo.GetCustomAttribute(typeof (CommandInfoAttribute), true) != null)
                    .Select(methodInfo => new Command(methodInfo, this)));
        }

        public void Run()
        {
            Processor.Reset();
            Processor.Run();
        }

        //protected IEnumerable<MethodInfo> GetCommands()
        //{
        //    return this.GetType()
        //        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        //        .Where(methodInfo => methodInfo.GetCustomAttribute(typeof(CommandInfoAttribute), true) != null);
        //}

        //private void InitilizateCommands()
        //{
        //    this.Commands = new ExpandoObject();
        //    var dictionary = (IDictionary<string, object>) this.Commands;

        //    foreach (var command in this.GetCommands())
        //    {
        //        dictionary[command.Name] = command.ReturnType;
        //    }
        //}

        #region Robot commands
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

        [CommandInfo(Name = "Направо", Description = "Повернуться на 90 против часовой")]
        public void Rightward()
        {
            Transform = Transform.Rotate(-90);
        }

        [CommandInfo(Name = "Налево", Description = "Повернуться на 90 по часовой")]
        public void Leftward()
        {
            Transform = Transform.Rotate(90);
        }
        #endregion
    }
}
