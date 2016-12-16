using System;
using System.Windows.Media.Imaging;

namespace TeachMe.Appl.Game.Robot.Command
{
    public class CommandViewer
    {
        public CommandViewer(Domain.Robot.Command command)
        {
            Command = command;
            Icon = new BitmapImage(new Uri("Game/Robot/Command/Icons/" + Command.Name + ".png", UriKind.Relative));
        }
        
        public Domain.Robot.Command Command { get; }
        public BitmapImage Icon { get; }

        public CommandViewer (CommandViewer commandViewer)
        {
            Command = commandViewer.Command;
            Icon = commandViewer.Icon;
        }
    }
}
