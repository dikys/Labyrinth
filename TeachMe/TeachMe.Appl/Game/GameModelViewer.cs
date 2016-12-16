using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeachMe.Appl.Game.Robot.Command;
using TeachMe.Domain;
using TeachMe.Domain.Robot;

namespace TeachMe.Appl.Game
{
    public class GameModelViewer
    {
        public GameModelViewer(GameModel gameModel, Canvas canvas, ListBox listForCurrentCommands, ListBox listForAvailableCommands)
        {
            _gameModel = gameModel;
            _canvas = canvas;

            listForCurrentCommands.ItemsSource =
                CurrentCommands = new ObservableCollection<CommandViewer>();

            listForAvailableCommands.ItemsSource =
                AvailableCommands =
                _gameModel.Robot.Commands.Select((command) => new CommandViewer(command)).ToList().AsReadOnly();

            DrawGame();
        }
        
        public ObservableCollection<CommandViewer> CurrentCommands { get; }
        public ReadOnlyCollection<CommandViewer> AvailableCommands { get; }
        
        private Canvas _canvas;
        private GameModel _gameModel;

        private double itemWidth;
        private double itemHeight;
        private Image robot;

        public void RunProgramm()
        {
            _gameModel.Robot.Processor.Reset();

            var timer = new Timer()
            {
                Interval = 500
            };
            timer.Elapsed += (s, a) =>
            {
                if (_gameModel.Robot.Processor.IsFinish)
                    timer.Stop();

                _gameModel.Robot.Processor.RunNext();
            };
            timer.Start();
        }

        public void DrawGame()
        {
            itemWidth = _canvas.ActualWidth / _gameModel.Field.Rows;
            itemHeight = _canvas.ActualHeight / _gameModel.Field.Colums;

            var sellImage = new BitmapImage(new Uri("Game/DefaultSell.png", UriKind.Relative));

            for (var x = 0; x < _gameModel.Field.Rows; x++)
            {
                for (var y = 0; y < _gameModel.Field.Colums; y++)
                {
                    var image = new Image()
                    {
                        Source = sellImage,
                        Width = itemWidth,
                        Height = itemHeight,
                        Stretch = Stretch.Fill
                    };
                    
                    _canvas.Children.Add(image);

                    Canvas.SetLeft(image, x * itemWidth);
                    Canvas.SetTop(image, y * itemHeight);
                }
            }

            robot = new Image()
            {
                Source = new BitmapImage(new Uri("Game/Robot/Robot.png", UriKind.Relative)),
                Width = itemWidth,
                Height = itemHeight,
                Stretch = Stretch.Fill
            };

            _canvas.Children.Add(robot);

            Canvas.SetLeft(robot, _gameModel.Robot.Transform.Location.X * itemWidth);
            Canvas.SetTop(robot, _gameModel.Robot.Transform.Location.Y * itemHeight);
        }
    }
}
