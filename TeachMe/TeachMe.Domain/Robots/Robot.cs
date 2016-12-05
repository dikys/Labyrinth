using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Domain.Robots;
using TeachMe.Infrastructure;

/*
 * Можно сделать потом разные виды микропроцессоров
*/

namespace TeachMe.Domain
{
    public class Robot
    {
        public Robot (Transform transform)
        {
            this.Processor = new MicroProcessor();

            this.Transform = transform;
        }

        public Transform Transform { get; private set; }
        public MicroProcessor Processor { get; private set; }

        public void Run()
        {
            this.Processor.Reset();
            this.Processor.Run();
        }

        //public dynamic Commands { get; protected set; }

        //private IEnumerable<MethodInfo> GetCommands()
        //{
        //    return this.GetType()
        //        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        //        .Where(methodInfo => methodInfo.GetCustomAttribute(typeof(RobotCommandInfoAttribute), true) != null);
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

        #region robot commands
        [RobotCommandInfo(Name = "Вперед", Description = "Двигаться на 1 клетку вперед")]
        public void Forward()
        {
            this.Transform = this.Transform + this.Transform.Forward;
        }

        [RobotCommandInfo(Name = "Назад", Description = "Двигаться на 1 клетку назад")]
        public void Backward()
        {
            this.Transform = this.Transform - this.Transform.Forward;
        }

        [RobotCommandInfo(Name = "Направо", Description = "Повернуться на 90 против часовой")]
        public void Rigthward()
        {
            this.Transform = this.Transform.Rotate(-90);
        }

        [RobotCommandInfo(Name = "Налево", Description = "Повернуться на 90 по часовой")]
        public void Leftward()
        {
            this.Transform = this.Transform.Rotate(90);
        }
        #endregion
    }
}
