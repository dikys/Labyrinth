using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeachMe.Domain;
using TeachMe.Infrastructure;

namespace TeachMe.DomainTest
{
    [TestClass]
    public class RobotProcessorTest
    {
        public void TestProgramm(Robot robot, Func<Robot, bool> check, params Action[] commands)
        {
            robot.Processor.Commands.AddRange(commands);
            robot.Processor.Run();
            Assert.IsTrue(check(robot));
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandForward()
        {
            var robot = new Robot(new Transform(new Location(), new Rotation(Angles.Right)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(1, 0), new Rotation(Angles.Right))),
                robot.Forward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, 1), new Rotation(Angles.Up))),
                robot.Forward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(-1, 0), new Rotation(Angles.Left))),
                robot.Forward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, -1), new Rotation(Angles.Down))),
                robot.Forward);
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandBackward()
        {
            var robot = new Robot(new Transform(new Location(), new Rotation(Angles.Right)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(-1, 0), new Rotation(Angles.Right))),
                robot.Backward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, -1), new Rotation(Angles.Up))),
                robot.Backward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(1, 0), new Rotation(Angles.Left))),
                robot.Backward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(0, 1), new Rotation(Angles.Down))),
                robot.Backward);
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandRightward()
        {
            var robot = new Robot(new Transform(new Location(), new Rotation(Angles.Right)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Down))),
                robot.Rigthward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Right))),
                robot.Rigthward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Up))),
                robot.Rigthward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Left))),
                robot.Rigthward);
        }

        [TestMethod]
        public void Should_CorrectTransform_When_CommandLeftward()
        {
            var robot = new Robot(new Transform(new Location(), new Rotation(Angles.Right)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Up))),
                robot.Leftward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Up)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Left))),
                robot.Leftward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Left)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Down))),
                robot.Leftward);

            robot = new Robot(new Transform(new Location(), new Rotation(Angles.Down)));
            TestProgramm(robot,
                (r) => r.Transform.Equals(new Transform(new Location(), new Rotation(Angles.Right))),
                robot.Leftward);
        }
    }
}
