using System;
using System.Windows;
using TeachMe.Domain;
using TeachMe.Infrastructure;

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.MouseLeftButtonDown += (sender, args) => this.DragMove();
            this.foldingButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
            this.minimizedAndMaximizedButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.closingButton.Click += (sender, args) => this.Close();

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

            var gameModel = new GameModel(robot,
                new Field(5, 5));

            this.currentProgramm.DataContext = robot.Processor.Commands;
        }
    }
}
