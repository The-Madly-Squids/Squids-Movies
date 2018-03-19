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
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class LoginWindow : Page
    {
        private IEngine engine;
        public LoginWindow()
        {
            InitializeComponent();
            EmailLoginTB.Focus();
            Initialize();
        }

        private void Initialize()
        {

            Init();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            this.engine = container.Resolve<IEngine>();

        }

        private static void Init()
        {
            AutomapperConfiguration.Initialize();
        }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            var email = this.EmailLoginTB.Text;
            var password = this.PasswordLoginTB.Password.ToString();
            this.engine.Start(email, password);

            this.NavigationService.Navigate(new ProfileWindow());
        }

        private void RegisterLinkClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterWindow());
        }


    }
}
