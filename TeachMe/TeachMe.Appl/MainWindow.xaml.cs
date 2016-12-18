using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TeachMe.Appl.Exception;
using TeachMe.Appl.Game;
using TeachMe.Appl.Game.Robot.Command;
using TeachMe.Domain;
using TeachMe.Domain.Field;
using TeachMe.Domain.Robot;
using TeachMe.Infrastructure;
using Transform = TeachMe.Infrastructure.Transform;

/*
 * Пожелания:
 * 
 * 
 * Во время выполнения команд заблокировать листбоксы
 * Кнопку сделать отстановить 
 * 
 * Разные типы ячеек нужны
 * 
 * Создать экран загрузки! (не важно)
 * 
 * Сделать цель на уровне
 * 
 * Сделать несколько уровней! 
 */

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameModelViewer GameModelViewer;
        
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                GameModelViewer =
                    new GameModelViewer(
                        new GameModel(
                            new MobileRobot(
                                new Transform(new Location(1, 1))),
                            new Field(4)),
                        MainCanvas,
                        CurrentCommands,
                        AvailableCommands);

                GameModelViewer.MobileRobotViewer.EndProgramm += () =>
                {
                    CurrentCommands.IsEnabled = true;
                    AvailableCommands.IsEnabled = true;
                };
            };

            RunProgramm.Click += (sender, args) =>
            {
                GameModelViewer.RunProgramm();

                CurrentCommands.IsEnabled = false;
                AvailableCommands.IsEnabled = false;
            };

            MouseLeftButtonDown += (sender, args) => DragMove();
            
            AvailableCommands.PreviewMouseLeftButtonDown += (sender, args) =>
            {
                var command = GetCommandViewerInListBox(AvailableCommands, args);

                if (command == null)
                    return;
                
                DragDrop.DoDragDrop(AvailableCommands, command, DragDropEffects.Copy);
            };

            CurrentCommands.PreviewMouseLeftButtonDown += (sender, args) =>
            {
                var command = GetCommandViewerInListBox(CurrentCommands, args);

                if (command == null)
                    return;

                GameModelViewer
                    .MobileRobotViewer
                    .CurrentCommands
                    .RemoveAt(GameModelViewer
                        .MobileRobotViewer
                        .CurrentCommands
                        .IndexOf(command));
                
                DragDrop.DoDragDrop(CurrentCommands, command, DragDropEffects.Move);
            };

            CurrentCommands.Drop += (sender, args) =>
            {
                if (!args.Data.GetDataPresent(typeof(CommandViewer)))
                    return;

                var draggedCommand = (CommandViewer)args.Data.GetData(typeof(CommandViewer));

                if (!GameModelViewer.MobileRobotViewer.CurrentCommands.Any())
                {
                    GameModelViewer
                        .MobileRobotViewer
                        .CurrentCommands
                        .Add(new CommandViewer(draggedCommand));

                    return;
                }

                var hitCommand = GetCommandViewerInListBox(CurrentCommands, args);

                if (hitCommand != null)
                {
                    GameModelViewer
                        .MobileRobotViewer
                        .CurrentCommands
                        .Insert(GameModelViewer.MobileRobotViewer.CurrentCommands.IndexOf(hitCommand),
                            new CommandViewer(draggedCommand));
                }
                else
                {
                    GameModelViewer
                        .MobileRobotViewer
                        .CurrentCommands
                        .Add(new CommandViewer(draggedCommand));
                }
            };

            // Для верхних трех кнопочек
            /*this.FoldingButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
            this.MinimizedAndMaximizedButton.Click +=
                (sender, args) =>
                    this.WindowState =
                        this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.ClosingButton.Click += (sender, args) => this.Close();*/

            //CurrentProgramm.ItemsSource = CurrentCommands;
        }

        private CommandViewer GetCommandViewerInListBox(ListBox listBox, MouseButtonEventArgs mouse)
        {
            return GetCommandViewerInListBox(listBox, mouse.GetPosition(listBox));
        }
        private CommandViewer GetCommandViewerInListBox(ListBox listBox, DragEventArgs element)
        {
            return GetCommandViewerInListBox(listBox, element.GetPosition(listBox));
        }
        private CommandViewer GetCommandViewerInListBox(ListBox listBox, Point checkedLocation)
        {
            var element = listBox.InputHitTest(checkedLocation) as UIElement;

            while (element != null)
            {
                if (element is ListBoxItem)
                {
                    var listBoxItem = (ListBoxItem)element;

                    if (!(listBoxItem.Content is CommandViewer))
                    {
                        throw new SearchCommandViewerInListBoxException();
                    }

                    var command = (CommandViewer)listBoxItem.Content;

                    return command;
                }

                element = VisualTreeHelper.GetParent(element) as UIElement;

                if (element is ListBox)
                    return null;
            }

            return null;
        }
    }
}
