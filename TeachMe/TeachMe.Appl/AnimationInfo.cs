using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace TeachMe.Appl
{
    public class AnimationInfo
    {
        public AnimationInfo(string name, Uri path, bool repeatBehavior = false)
        {
            Name = name;

            Animation = new GifBitmapDecoder(path, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            FrameIndexAnimation = new Int32Animation(0,
                Animation.Frames.Count - 1,
                new Duration(TimeSpan.FromMilliseconds(Animation.Frames.Count * _oneFrameTime)));

            if (repeatBehavior)
                FrameIndexAnimation.RepeatBehavior = RepeatBehavior.Forever;
        }

        public string Name { get; }
        public GifBitmapDecoder Animation { get; }
        public Int32Animation FrameIndexAnimation { get; }

        static AnimationInfo()
        {
            _oneFrameTime = 500;
        }
        private static double _oneFrameTime;
    }
}
