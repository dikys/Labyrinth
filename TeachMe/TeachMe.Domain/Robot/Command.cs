using System;
using System.Linq;
using System.Reflection;

namespace TeachMe.Domain.Robot
{
    public class Command
    {
        public Command(MethodInfo methodInfo, object target)
        {
            Name = methodInfo.Name;
            Method = (Action)methodInfo.CreateDelegate(typeof(Action), target);
            Attributes =
                        methodInfo.GetCustomAttributes(typeof(CommandInfoAttribute), true)
                            .Cast<CommandInfoAttribute>()
                            .Single();
        }

        public String Name { get; }
        public Action Method { get; }
        public CommandInfoAttribute Attributes { get; }
    }
}
