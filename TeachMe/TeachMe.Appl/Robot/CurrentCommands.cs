using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Appl.Robot
{
    public class CurrentCommands
    {
        public CurrentCommands()
        {
            Commands = new ObservableCollection<CommandViewer>();
        }

        public ObservableCollection<CommandViewer> Commands { get; }
    }
}
