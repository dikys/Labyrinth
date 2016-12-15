using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeachMe.Domain;
using Transform = TeachMe.Infrastructure.Transform;

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Command> RobotCommands;
        public ObservableCollection<Command> CurrentCommands; 

        private Command _dragged;

        private GameModel _gameModel;

        public MainWindow()
        {
            InitializeComponent();
            
            this.MouseLeftButtonDown += (sender, args) => this.DragMove();
            /*this.FoldingButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
            this.MinimizedAndMaximizedButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.ClosingButton.Click += (sender, args) => this.Close();*/
            
            this.AllCommands.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                if (this._dragged != null)
                    return;

                var element = this.AllCommands.InputHitTest(e.GetPosition(this.AllCommands)) as UIElement;

                while (element != null)
                {
                    if (element is ListBoxItem)
                    {
                        var item = (ListBoxItem) element;

                        if (!(item.Content is Command))
                            return;

                        var command = (Command)item.Content;

                        this._dragged = command;

                        /*var grid = (Grid) item.Content;
                        
                        var textBlockWithIndex = grid.Children[1] as TextBlock;

                        if (textBlockWithIndex == null)
                            throw new NullReferenceException("textBlockWithIndex");

                        int index;
                        if (!Int32.TryParse(textBlockWithIndex.Text, out index))
                            throw new FormatException();

                        MessageBox.Show(index.ToString());

                        return;*/

                        /*var rectangle = (ListBoxItem) element;

                        this._dragged = new Rectangle()
                        {
                            Width = rectangle.Width,
                            Height = rectangle.Height
                        };
                        
                        DragDrop.DoDragDrop(this._dragged, this._dragged, DragDropEffects.Move);

                        break;*/
                    }

                    element = VisualTreeHelper.GetParent(element) as UIElement;

                    if (element is ListBox)
                        return;
                }
            };
            
            this.CurrentProgramm.Drop += (sender, e) =>
            {
                //this.CurrentCommands.Add(this._dragged);
                this._dragged = null;

                if (!this.CurrentCommands.Any())
                    return;
                
                var element = AllCommands.InputHitTest(e.GetPosition(this.CurrentProgramm)) as UIElement;
                
                while (element != null)
                {
                    if (element is Rectangle)
                    {
                        var rectangle = (Rectangle)element;
                        
                        break;
                    }

                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }
            };

            var robot = new Robot(new Transform());
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
            
            this._gameModel = new GameModel(robot,
                new Field(5, 5));
            
            this.RobotCommands = new ObservableCollection<Command>
            {
                new Command(0,
                    "Вперед",
                    "Двигаться на 1 клетку вперед",
                    new BitmapImage(new Uri("CommandImages/Forward.png", UriKind.Relative)),
                    robot.Forward),
                new Command(1,
                    "Назад",
                    "Двигаться на 1 клетку назад",
                    new BitmapImage(new Uri("CommandImages/Backward.png", UriKind.Relative)),
                    robot.Forward)
            };
            this.CurrentCommands = new ObservableCollection<Command>();

            this.AllCommands.ItemsSource = this.RobotCommands;
            this.CurrentProgramm.ItemsSource = this.CurrentCommands;

            // Нужно бы сделать класс который подкачивает все команды робота и записывает их в нужный лист бокс)
        }
    }
}
