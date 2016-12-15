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
using TeachMe.Appl.Robot;
using TeachMe.Domain;
using TeachMe.Domain.Robot;
using Transform = TeachMe.Infrastructure.Transform;

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AvailableCommands AvailableCommands;
        //public ObservableCollection<CommandApp> CurrentCommands; 

        //private CommandApp _dragged;

        private GameModel _gameModel;

        public MainWindow()
        {
            InitializeComponent();
            
            MouseLeftButtonDown += (sender, args) => DragMove();
            /*this.FoldingButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
            this.MinimizedAndMaximizedButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.ClosingButton.Click += (sender, args) => this.Close();*/
            
            /*AllCommands.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                if (_dragged != null)
                    return;

                var element = AllCommands.InputHitTest(e.GetPosition(AllCommands)) as UIElement;

                while (element != null)
                {
                    if (element is ListBoxItem)
                    {
                        var item = (ListBoxItem) element;

                        if (!(item.Content is CommandApp))
                            return;

                        var command = (CommandApp)item.Content;

                        _dragged = command;

                        DragDrop.DoDragDrop(item, command, DragDropEffects.Copy);
                    }

                    element = VisualTreeHelper.GetParent(element) as UIElement;

                    if (element is ListBox)
                        return;
                }
            };
            
            CurrentProgramm.Drop += (sender, e) =>
            {
                _dragged = null;

                if (!e.Data.GetDataPresent(typeof (CommandApp)))
                    return;

                var draggedCommand = (CommandApp) e.Data.GetData(typeof(CommandApp));
                
                if (!CurrentCommands.Any())
                {
                    CurrentCommands.Add(draggedCommand);

                    return;
                }

                var element = CurrentProgramm.InputHitTest(e.GetPosition(CurrentProgramm)) as UIElement;
                CommandApp hitCommandApp = null;

                while (element != null)
                {
                    if (element is ListBoxItem)
                    {
                        var item = (ListBoxItem)element;

                        if (!(item.Content is CommandApp))
                            return;

                        hitCommandApp = (CommandApp)item.Content;
                    }

                    element = VisualTreeHelper.GetParent(element) as UIElement;

                    if (element is ListBox)
                        break;
                }

                if (hitCommandApp != null)
                {
                    MessageBox.Show(CurrentCommands.IndexOf(hitCommandApp).ToString());

                    CurrentCommands.Insert(CurrentCommands.IndexOf(hitCommandApp),
                        draggedCommand);
                }
                else
                {
                    CurrentCommands.Add(draggedCommand);
                }
            };*/

            var robot = new MobileRobot(new Transform());
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
            
            _gameModel = new GameModel(robot,
                new Field(5, 5));

            AvailableCommands = new AvailableCommands(robot);
            //CurrentCommands = new ObservableCollection<CommandApp>();

            AllCommands.ItemsSource = AvailableCommands.Commands;
            //CurrentProgramm.ItemsSource = CurrentCommands;

            // Нужно бы сделать класс который подкачивает все команды робота и записывает их в нужный лист бокс)
            // Для верхнего листбокса убрать подсказки
            // Для нижнего оставить
        }
    }
}
