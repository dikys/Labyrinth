using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TeachMe.Infrastructure;

namespace TeachMe.Appl
{
    public class Animator : Image
    {
        public Animator(Size size)
        {
            Width = size.Width;
            Height = size.Height;
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new TransformGroup()
            {
                Children =
                {
                    new ScaleTransform(1, -1),
                    new RotateTransform(90)
                }
            };
            Stretch = Stretch.Fill;

            Animations = new List<AnimationInfo>();
        }

        public int FrameIndex
        {
            get { return (int)GetValue(FrameIndexProperty); }
            set { SetValue(FrameIndexProperty, value); }
        }
        public List<AnimationInfo> Animations { get; }
        private int _currentAnimationIndex;
        
        static Animator()
        {
            FrameIndexProperty = DependencyProperty.Register("FrameIndex",
                typeof(int),
                typeof(Animator),
                new UIPropertyMetadata(0,
                    new PropertyChangedCallback(
                        (sender, args) =>
                        {
                            Animator animator = (Animator)sender;

                            animator.SetFrame((int)args.NewValue);
                        })));

            RotationPropertyPath = "RenderTransform.Children[1]";
        }
        public static readonly DependencyProperty FrameIndexProperty;
        public static readonly String RotationPropertyPath;

        public void PlayAnimation(string animationName)
        {
            var animationIndex = Animations.IndexOf(Animations.Single(animationInfo => animationInfo.Name == animationName));

            PlayAnimation(animationIndex);
        }
        public void PlayAnimation(int animationIndex)
        {
            if (!(0 <= animationIndex && animationIndex < Animations.Count))
                throw new ArgumentOutOfRangeException("animationIndex should be between 0 and " + Animations.Count);
            
            if (_currentAnimationIndex == animationIndex)
                return;

            _currentAnimationIndex = animationIndex;

            BeginAnimation(FrameIndexProperty, Animations[animationIndex].FrameIndexAnimation);
        }

        public void PlayTransformAnimation(double x, double y, double angle, Duration duration)
        {
            var transformAnimation = new Storyboard();
            
            if (x != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = x,
                    Duration = duration
                };
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));

                transformAnimation.Children.Add(animation);
            }
            if (y != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = y,
                    Duration = duration
                };
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

                transformAnimation.Children.Add(animation);
            }
            if (angle != 0)
            {
                var animation = new DoubleAnimation()
                {
                    By = angle,
                    Duration = duration
                };

                Storyboard.SetTargetProperty(animation, new PropertyPath(Animator.RotationPropertyPath + ".Angle"));

                transformAnimation.Children.Add(animation);
            }

            transformAnimation.Begin(this);
        }

        private void SetFrame(int frameIndex)
        {
            Source = Animations[_currentAnimationIndex].Animation.Frames[frameIndex];
            InvalidateVisual();
        }
    }
}
