using System;
using System.Windows.Media.Imaging;

namespace TeachMe.Appl.Game.Robot.Command
{
    public class CommandViewer
    {
        public CommandViewer(Domain.Robot.Command command)
        {
            Command = command;
            Icon = new BitmapImage(new Uri(PathToCommandIcons + Command.Name + ".png"));
        }
        public CommandViewer(CommandViewer commandViewer)
        {
            Command = commandViewer.Command;
            Icon = commandViewer.Icon;
        }

        public Domain.Robot.Command Command { get; }
        public BitmapImage Icon { get; }

        static CommandViewer()
        {
            PathToCommandIcons = "pack://application:,,,/Game/Robot/Command/Icons/";
            PathToCommandAnimations = "pack://application:,,,/Game/Robot/Images/";
        }

        public static string PathToCommandIcons { get; }
        public static string PathToCommandAnimations { get; }
    }
}
