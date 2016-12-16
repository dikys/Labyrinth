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

            Commands.CollectionChanged += (sender, e) =>
            {
                var indexChanging = 0;

                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        e.NewItems.Cast<CommandViewer>().Select((command, index) => command.Index = e.NewStartingIndex + index);

                        indexChanging = e.NewStartingIndex;

                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:

                        indexChanging = e.OldStartingIndex;
                        break;
                }

                for (var index = indexChanging; index < Commands.Count; index++)
                {
                    Commands[index].Index = index;
                }

                //var index2 = 0;
                //Commands.ToList().ForEach((command) => Console.WriteLine("Актуальная = " + command.Index + " реальная = " + index2++));
            };
        }

        public ObservableCollection<CommandViewer> Commands { get; }
    }
}
