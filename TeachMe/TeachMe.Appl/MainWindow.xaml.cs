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
using TeachMe.Appl.Exception;
using TeachMe.Appl.Robot;
using TeachMe.Domain;
using TeachMe.Domain.Robot;
using Transform = TeachMe.Infrastructure.Transform;

/*
 * Пожелания:
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
        public GameModelViewer gameModelViewer;
        
        public MainWindow()
        {
            InitializeComponent();

            gameModelViewer = new GameModelViewer(new GameModel(new MobileRobot(), new Field(5)), CurrentCommands, AvailableCommands);

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

                gameModelViewer.CurrentCommands.RemoveAt(gameModelViewer.CurrentCommands.IndexOf(command));

                DragDrop.DoDragDrop(CurrentCommands, command, DragDropEffects.Move);
            };

            CurrentCommands.Drop += (sender, args) =>
            {
                if (!args.Data.GetDataPresent(typeof(CommandViewer)))
                    return;

                var draggedCommand = (CommandViewer)args.Data.GetData(typeof(CommandViewer));

                if (!gameModelViewer.CurrentCommands.Any())
                {
                    gameModelViewer.CurrentCommands.Add(new CommandViewer(draggedCommand));

                    return;
                }

                var hitCommand = GetCommandViewerInListBox(CurrentCommands, args);

                if (hitCommand != null)
                {
                    gameModelViewer.CurrentCommands.Insert(gameModelViewer.CurrentCommands.IndexOf(hitCommand), new CommandViewer(draggedCommand));
                }
                else
                {
                    gameModelViewer.CurrentCommands.Add(new CommandViewer(draggedCommand));
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
