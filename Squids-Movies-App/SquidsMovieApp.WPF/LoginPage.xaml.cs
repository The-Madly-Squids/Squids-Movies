using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using SquidsMovieApp.Common;
using SquidsMovieApp.Core;
using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;

namespace SquidsMovieApp.WPF
{
    public partial class LoginPage : Page
    {
        private BackgroundWorker worker;
        private LoadingWindow loadingWindow;

        public LoginPage()
        {
            InitializeComponent();
            EmailLoginTB.Focus();
            RegisterContainer();
        }

        public IMainController MainController { get; private set; }
        public AuthProvider AuthProvider { get; private set; }

        private void RegisterContainer()
        {
            AutomapperConfiguration.Initialize();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            this.MainController = container.Resolve<IMainController>();
            this.AuthProvider = container.Resolve<AuthProvider>();
        }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            //var email = this.EmailLoginTB.Text;
            //var password = this.PasswordLoginTB.Password.ToString();

            //this.NavigationService.Navigate(new ProfilePage(this.MainController, AuthProvider));

            this.worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            this.loadingWindow = new LoadingWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            worker.RunWorkerAsync();
            loadingWindow.ShowDialog();
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Thread.Sleep(1000);
            //}
        }

        private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            loadingWindow.Hide();
        }

        private void RegisterLinkClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage(this.MainController, AuthProvider));
        }
    }
}
