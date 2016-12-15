﻿using System;
using System.Windows.Media.Imaging;
using TeachMe.Domain.Robot;

namespace TeachMe.Appl.Robot
{
    public class CommandViewer
    {
        public CommandViewer(Command command)
        {
            Command = command;
            Icon = new BitmapImage(new Uri("Robot/CommandImages/" + Command.Name + ".png", UriKind.Relative));
        }

        public Command Command { get; }
        public BitmapImage Icon { get; }
    }
}