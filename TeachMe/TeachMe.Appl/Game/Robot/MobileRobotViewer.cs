using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TeachMe.Appl.Game.Robot.Command;
using TeachMe.Domain.Robot;
using Timer = System.Timers.Timer;

namespace TeachMe.Appl.Game.Robot
{
    public class MobileRobotViewer
    {
        public MobileRobotViewer(MobileRobot robot, Size robotSize, Duration animationDuration)
        {
            _robot = robot;

            Size = robotSize;
            AnimationDuration = animationDuration;

            Animator = new Animator(Size);
            Canvas.SetLeft(Animator, _robot.Transform.Location.X * Size.Width);
            Canvas.SetTop(Animator, _robot.Transform.Location.Y * Size.Height);

            AvailableCommands = robot.AvailableCommands.Select(command =>
            {
                var commandViewer = new CommandViewer(command);
                
                Animator.Animations.Add(
                    new AnimationInfo(commandViewer.Command.Name,
                        new Uri(CommandViewer.PathToCommandAnimations + commandViewer.Command.Name + ".gif")));

                return commandViewer;
            }).ToList().AsReadOnly();

            CurrentCommands = new ObservableCollection<CommandViewer>();

            Animator.PlayAnimation(AvailableCommands[0].Command.Name);
        }
        
        private MobileRobot _robot;

        public ObservableCollection<CommandViewer> CurrentCommands { get; }
        public ReadOnlyCollection<CommandViewer> AvailableCommands { get; }

        public Animator Animator { get; }

        public Size Size { get; }
        public Duration AnimationDuration { get; }

        public bool IsRun { get; private set; }

        public event Action EndProgramm;

        public void ExecuteCurrentCommands()
        {
            if (IsRun)
                return;

            if (!CurrentCommands.Any())
                return;

            IsRun = true;

            _robot.ClearProgramm();
            _robot.AddCommands(CurrentCommands.Select(commandViewer => commandViewer.Command));
            
            var timer = new Timer(AnimationDuration.TimeSpan.TotalMilliseconds);
            timer.Elapsed += (sender, args) =>
            {
                Infrastructure.Transform beforeTransform = _robot.Transform;
                _robot.RunNextCommand();

                Animator.Dispatcher.BeginInvoke(new Action<int, Infrastructure.Transform>(AnimateRobot),
                    _robot.CurrentCommandNumber - 1,
                    beforeTransform);

                if (_robot.IsProgrammEnd)
                {
                    timer.Stop();

                    if (EndProgramm != null)
                    {
                        Thread.Sleep((int)AnimationDuration.TimeSpan.TotalMilliseconds);

                        Animator.Dispatcher.BeginInvoke(EndProgramm);
                    }
                }
            };
            timer.Start();

            IsRun = false;
        }

        private void AnimateRobot(int commandIndex, Infrastructure.Transform beforeTransform)
        {
            var deltaLocation = _robot.Transform.Location - beforeTransform.Location;
            var deltaAngle = (beforeTransform.Rotation - _robot.Transform.Rotation) * 180 / Math.PI;
            Animator.PlayTransformAnimation(deltaLocation.X * Size.Width, deltaLocation.Y * Size.Height, deltaAngle, AnimationDuration);

            Animator.PlayAnimation(CurrentCommands[commandIndex].Command.Name);
        }
    }
}
