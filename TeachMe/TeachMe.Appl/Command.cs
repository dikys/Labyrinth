using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TeachMe.Appl
{
    public class Command
    {
        // ТУТ ПРОБЛЕМКА ПОТОМ БУДЕТ куда индекс девать? Надо присваивать где-то и изменять нужно
        // А если его нужно менять, то сделать проверку целостности
        public Command(Command command)
        {
            this.Name = command.Name;
            this.Description = command.Description;
            this.Image = command.Image;
            this.Method = command.Method;
        }

        public Command(int index, string name, string description, BitmapImage image, Action method)
        {
            this.Index = index;
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Method = method;
        }
        
        public int Index { get; }
        public string Description { get; }
        public string Name { get; }
        public BitmapImage Image { get; }
        public Action Method { get; }
    }
}
