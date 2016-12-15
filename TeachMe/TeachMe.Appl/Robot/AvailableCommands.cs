﻿using System.Collections.Generic;
using System.Linq;
using TeachMe.Domain.Robot;

namespace TeachMe.Appl.Robot
{
    public class AvailableCommands
    {
        public AvailableCommands(MobileRobot robot)
        {
            Commands = robot.Commands.Select(command => new CommandViewer(command)).ToList();
        }

        public List<CommandViewer> Commands { get; }
    }
}