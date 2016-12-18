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
        private Transform _transform;
        public Transform Transform
        {
            get { return _transform; }
            private set { _transform = value; }
        }
        public MicroProcessor Processor { get; }
        public List<Command> Commands { get; }

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
            Processor.Run();
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
