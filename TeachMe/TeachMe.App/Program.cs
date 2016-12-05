﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeachMe.Domain;
using TeachMe.Infrastructure;

namespace TeachMe.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot(new Transform());
            robot.Processor.Commands.AddRange(new Action[]
            {
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Leftward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Leftward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Leftward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Forward,
                robot.Leftward
            });

            var gameModel = new GameModel(robot,
                new Field(5, 5));
            
            Application.Run(new MainForm(gameModel));
        }
    }
}
