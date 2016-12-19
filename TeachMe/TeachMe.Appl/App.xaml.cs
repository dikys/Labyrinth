using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TeachMe.Appl
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*protected override void OnStartup(StartupEventArgs e)
        {
            var splash = new Window1();
            splash.Show();

            var timer = new Stopwatch();
            timer.Start();

            base.OnStartup(e);

            var main = new MainWindow();
            timer.Stop();

            int time = 4000 - (int) timer.ElapsedMilliseconds;
            if (time > 0)
                Thread.Sleep(time);

            splash.Close();
        }*/
    }
}
