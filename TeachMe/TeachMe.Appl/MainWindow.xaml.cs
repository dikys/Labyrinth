using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            this.FoldingButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
            this.MinimizedAndMaximizedButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.ClosingButton.Click += (sender, args) => this.Close();

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

            var data = new List<Button>();
            data.Add(new Button {Content = "Нажми на меня"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});
            data.Add(new Button {Content = "Нажми на меня 2"});

            this.CurrentProgramm.ItemsSource = data;
        }
    }
}
