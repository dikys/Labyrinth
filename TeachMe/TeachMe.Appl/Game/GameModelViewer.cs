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
        public GameModelViewer(GameModel gameModel, Canvas canvas, ListBox listForCurrentCommands, ListBox listForAvailableCommands)
        {
            _gameModel = gameModel;
            
            _canvas = canvas;
            _canvas.LayoutTransform = new TransformGroup()
            {
                Children = new TransformCollection()
                {
                    new ScaleTransform(1, -1),
                    new TranslateTransform(0, -_canvas.ActualHeight)
                }
            };
            _itemWidth = _canvas.ActualWidth / _gameModel.Field.Rows;
            _itemHeight = _canvas.ActualHeight / _gameModel.Field.Colums;

            MobileRobotViewer = new MobileRobotViewer(gameModel.Robot,
                new Size(_itemWidth, _itemHeight),
                new Duration(TimeSpan.FromMilliseconds(1000)));

            listForCurrentCommands.ItemsSource = MobileRobotViewer.CurrentCommands;
            listForAvailableCommands.ItemsSource = MobileRobotViewer.AvailableCommands;

            DrawGame();
        }
        private Canvas _canvas;
        private GameModel _gameModel;
        public MobileRobotViewer MobileRobotViewer { get; }

        private double _itemWidth;
        private double _itemHeight;
        
        private void DrawGame()
        {
            var sellImage = new BitmapImage(new Uri("Game/Field/DefaultSell.png", UriKind.Relative));

            for (var x = 0; x < _gameModel.Field.Rows; x++)
            {
                for (var y = 0; y < _gameModel.Field.Colums; y++)
                {
                    var image = new Image()
                    {
                        Source = sellImage,
                        Width = _itemWidth,
                        Height = _itemHeight,
                        Stretch = Stretch.Fill,
                        LayoutTransform = new ScaleTransform(1, -1)
                    };
                    
                    _canvas.Children.Add(image);

                    Canvas.SetLeft(image, x * _itemWidth);
                    Canvas.SetTop(image, y * _itemHeight);
                }
            }
            
            _canvas.Children.Add(MobileRobotViewer.Animator);
        }

        public void RunProgramm()
        {
            MobileRobotViewer.RunProgramm();
        }
    }
}
