using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeachMe.Appl.Robot;
using TeachMe.Domain;

namespace TeachMe.Appl
{
    public class GameModelViewer
    {
        public GameModelViewer(GameModel gameModel, ListBox listForCurrentCommands, ListBox listForAvailableCommands)
        {
            _gameModel = gameModel;

            listForCurrentCommands.ItemsSource =
                CurrentCommands = new ObservableCollection<CommandViewer>();

            listForAvailableCommands.ItemsSource =
                AvailableCommands =
                _gameModel.Robot.Commands.Select((command) => new CommandViewer(command)).ToList().AsReadOnly();
        }
        
        public ObservableCollection<CommandViewer> CurrentCommands { get; }
        public ReadOnlyCollection<CommandViewer> AvailableCommands { get; } 

        private GameModel _gameModel;
    }
}
