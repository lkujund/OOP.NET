using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WorldCup_WPF.WPFWindows;
using WorldCupDAL;

namespace WorldCup_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Repository.IsConfigured() && Repository.ResolutionSet())
            {
                new MainWindow().Show();
            }
            else
            {
                new ConfigWindow().Show();
            }
        }
    }
}
