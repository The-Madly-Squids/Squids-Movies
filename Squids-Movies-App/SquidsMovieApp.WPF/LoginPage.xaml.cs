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
using SquidsMovieApp.Core.Contracts;
using SquidsMovieApp.Program.Controllers;

namespace SquidsMovieApp.WPF
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            EmailLoginTB.Focus();
            this.Engine = ((MainWindow)Application.Current.MainWindow).Engine;
        }

        public IEngine Engine { get; private set; }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            var email = this.EmailLoginTB.Text;
            var password = this.PasswordLoginTB.Password.ToString();

            this.NavigationService.Navigate(new ProfilePage());
        }

        private void RegisterLinkClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }


    }
}
