using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeachMe.Appl.Game.Robot;
using TeachMe.Domain;

namespace TeachMe.Appl.Game
{
    public class GameModelViewer
    {
        public GameModelViewer(GameModel gameModel, Canvas canvas)
        {
            _gameModel = gameModel;
            
            ItemSize = new Size(canvas.ActualWidth / _gameModel.Field.Rows,
                canvas.ActualHeight / _gameModel.Field.Colums);

            MobileRobotViewer = new MobileRobotViewer(gameModel.Robot,
                ItemSize,
                new Duration(TimeSpan.FromMilliseconds(1000)));

            DrawGame(canvas);
        }

        private readonly GameModel _gameModel;
        public MobileRobotViewer MobileRobotViewer { get; }
        public Size ItemSize { get; }
        
        private void DrawGame(Canvas canvas)
        {
            var sellImage = new BitmapImage(new Uri("Game/Field/DefaultSell.png", UriKind.Relative));

            for (var x = 0; x < _gameModel.Field.Rows; x++)
            {
                for (var y = 0; y < _gameModel.Field.Colums; y++)
                {
                    var image = new Image()
                    {
                        Source = sellImage,
                        Width = ItemSize.Width,
                        Height = ItemSize.Height,
                        Stretch = Stretch.Fill,
                        LayoutTransform = new ScaleTransform(1, -1)
                    };

                    canvas.Children.Add(image);

                    Canvas.SetLeft(image, x * ItemSize.Width);
                    Canvas.SetTop(image, y * ItemSize.Height);
                }
            }

            canvas.Children.Add(MobileRobotViewer.Animator);
        }

        public void RunProgramm()
        {
            MobileRobotViewer.ExecuteCurrentCommands();
        }
    }
}
