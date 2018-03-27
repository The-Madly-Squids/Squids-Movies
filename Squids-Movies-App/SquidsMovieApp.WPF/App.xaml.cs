using Autofac;
using SquidsMovieApp.Common;
using SquidsMovieApp.Core.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace SquidsMovieApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ExitBtnClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //AutomapperConfiguration.Initialize();
            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //var container = builder.Build();
            //var mainWindow = container.Resolve<MainWindow>();

            //mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
