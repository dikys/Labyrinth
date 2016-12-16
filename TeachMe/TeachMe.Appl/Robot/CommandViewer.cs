using System;
using System.Windows.Media.Imaging;
using TeachMe.Domain.Robot;

namespace TeachMe.Appl.Robot
{
    public class CommandViewer
    {
        public CommandViewer(Command command, int index)
        {
            Index = index;
            Command = command;
            Icon = new BitmapImage(new Uri("Robot/CommandImages/" + Command.Name + ".png", UriKind.Relative));
        }
        
        public int Index { set; get; }
        public Command Command { get; }
        public BitmapImage Icon { get; }

        public CommandViewer (CommandViewer commandViewer)
        {
            Index = commandViewer.Index;
            Command = commandViewer.Command;
            Icon = commandViewer.Icon;
        }

        protected bool Equals (CommandViewer other)
        {
            return Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CommandViewer)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Index;
        }
    }
}
