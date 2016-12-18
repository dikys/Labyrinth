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

            AvailableCommands = robot.Commands.Select(command =>
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

        public void RunProgramm()
        {
            if (IsRun)
                return;

            if (!CurrentCommands.Any())
                return;

            IsRun = true;

            _robot.Processor.Commands.Clear();
            _robot.Processor.Reset();
            _robot.Processor.Commands.AddRange(CurrentCommands.Select(commandViewer => commandViewer.Command.Method));
            
            var timer = new Timer(AnimationDuration.TimeSpan.TotalMilliseconds);
            timer.Elapsed += (sender, args) =>
            {
                Infrastructure.Transform beforeTransform = _robot.Transform;
                _robot.Processor.RunNext();

                Animator.Dispatcher.BeginInvoke(new Action<int, Infrastructure.Transform>(AnimateRobot),
                    _robot.Processor.CurrentCommandNumber - 1,
                    beforeTransform);

                if (_robot.Processor.IsFinish)
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

        private void AnimateRobot(int commandIndex, Infrastructure.Transform begoreTransform)
        {
            var transformAnimation = CreateTransformAnimation(Size, _robot.Transform, begoreTransform, AnimationDuration);
            
            transformAnimation.Begin(Animator);

            Animator.PlayAnimation(CurrentCommands[commandIndex].Command.Name);
        }

        private Storyboard CreateTransformAnimation(Size robotSize,
            Infrastructure.Transform currentTransform,
            Infrastructure.Transform beforeTransform,
            Duration duration)
        {
            var result = new Storyboard();

            var deltaLocation = currentTransform.Location - beforeTransform.Location;
            var deltaAngle = (beforeTransform.Rotation - currentTransform.Rotation) * 180 / Math.PI;

            if (deltaLocation.X != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = deltaLocation.X * robotSize.Width,
                    Duration = duration
                };
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));

                result.Children.Add(animation);
            }
            if (deltaLocation.Y != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = deltaLocation.Y * robotSize.Height,
                    Duration = duration
                };
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

                result.Children.Add(animation);
            }
            if ((int)deltaAngle != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = deltaAngle,
                    Duration = duration
                };

                Storyboard.SetTargetProperty(animation, new PropertyPath(Animator.RotationPropertyPath + ".Angle"));

                result.Children.Add(animation);
            }

            return result;
        }
    }
}
