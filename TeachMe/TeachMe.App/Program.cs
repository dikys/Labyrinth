using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TeachMe.Domain;
using TeachMe.Domain.Field;
using TeachMe.Domain.Robot;
using TeachMe.Infrastructure;

namespace TeachMe.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new MobileRobot(new Transform());
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
                new Field(5));
            
            Application.Run(new MainForm(gameModel));
        }
    }
}
