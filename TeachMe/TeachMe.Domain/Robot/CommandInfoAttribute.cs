using System;

namespace TeachMe.Domain.Robot
{
    public class CommandInfoAttribute : Attribute
    {
        public string Name { set; get; }
        public string Description { set; get; }
    }
}
