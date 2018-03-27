using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public LoginPage()
        {
            InitializeComponent();
            EmailLoginTB.Focus();
            RegisterContainer();
        }

        public IMainController MainController { get; private set; }

        private void RegisterContainer()
        {
            AutomapperConfiguration.Initialize();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            this.MainController = container.Resolve<IMainController>();
        }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            var email = this.EmailLoginTB.Text;
            var password = this.PasswordLoginTB.Password.ToString();

            this.NavigationService.Navigate(new ProfilePage(this.MainController));
        }

        private void RegisterLinkClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage(this.MainController, this));
        }
    }
}
