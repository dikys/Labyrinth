using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeachMe.Domain;
using TeachMe.Domain.Robot;
using TeachMe.Infrastructure;

namespace TeachMe.DomainTest
{
    [TestClass]
    public class RobotProcessorTest
    {
        public void TestProgramm(MobileRobot robot, Func<MobileRobot, bool> check, params Command[] commands)
        {
            robot.AddCommands(commands);
            robot.RunProgramm();
            Assert.IsTrue(check(robot));
        }

        public Command GetCommand(MobileRobot robot, string commandName)
        {
            return robot.AvailableCommands.Single(command => command.Name == commandName);
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandForward()
        {
            var robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Right)));

            Func<MobileRobot, Command> getTestingCommand = (r) => GetCommand(r, "Forward");

            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(1, 0), new Rotation(Angles.Right))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, 1), new Rotation(Angles.Up))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(-1, 0), new Rotation(Angles.Left))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, -1), new Rotation(Angles.Down))),
                getTestingCommand(robot));
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandBackward()
        {
            var robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Right)));

            Func<MobileRobot, Command> getTestingCommand = (r) => GetCommand(r, "Backward");

            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(-1, 0), new Rotation(Angles.Right))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, -1), new Rotation(Angles.Up))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(1, 0), new Rotation(Angles.Left))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, 1), new Rotation(Angles.Down))),
                getTestingCommand(robot));
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandRightward()
        {
            var robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Right)));

            Func<MobileRobot, Command> getTestingCommand = (r) => GetCommand(r, "Rightward");

            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Down))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Right))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Up))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Left))),
                getTestingCommand(robot));
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandLeftward()
        {
            var robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Right)));

            Func<MobileRobot, Command> getTestingCommand = (r) => GetCommand(r, "Leftward");

            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Up))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Left))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Down))),
                getTestingCommand(robot));

            robot = new MobileRobot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Right))),
                getTestingCommand(robot));
        }
    }
}
