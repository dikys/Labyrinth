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

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameModel _gameModel;
        private readonly AvailableCommands _availableCommands;
        private readonly CurrentCommands _currentCommands;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _gameModel = new GameModel(new MobileRobot(new Transform()), new Field(5, 5));
            _availableCommands = new AvailableCommands(_gameModel.Robot);
            _currentCommands = new CurrentCommands();

            CurrentCommands.ItemsSource = _currentCommands.Commands;
            AvailableCommands.ItemsSource = _availableCommands.Commands;

            MouseLeftButtonDown += (sender, args) => DragMove();

            AvailableCommands.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                var command = GetCommandViewerInListBox(AvailableCommands, e);

                DragDrop.DoDragDrop(AvailableCommands, command, DragDropEffects.Copy);
            };

            CurrentCommands.Drop += (sender, e) =>
            {
                if (!e.Data.GetDataPresent(typeof(CommandViewer)))
                    return;

                var draggedCommand = (CommandViewer)e.Data.GetData(typeof(CommandViewer));

                if (!_currentCommands.Commands.Any())
                {
                    _currentCommands.Commands.Add(draggedCommand);

                    return;
                }

                var hitCommand = GetCommandViewerInListBox(CurrentCommands, e);

                if (hitCommand != null)
                {
                    var index = 0;

                    _currentCommands.Commands.Insert(index, draggedCommand);
                }
                else
                {
                    _currentCommands.Commands.Add(draggedCommand);
                }
            };

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

            // Для верхнего листбокса убрать подсказки создать новое свойство и пускай оно на все реагирует
            // Для нижнего оставить
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
