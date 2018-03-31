using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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
using SquidsMovieApp.Common.Exceptions;
using SquidsMovieApp.Core;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;

namespace SquidsMovieApp.WPF
{
    public partial class LoginPage : Page
    {
        private BackgroundWorker worker;
        private LoadingWindow loadingWindow;
        private string email;
        private string password;

        public LoginPage()
        {
            InitializeComponent();
            EmailLoginTB.Focus();
            RegisterContainer();

            this.worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        public IMainController MainController { get; private set; }
        public UserContext UserContext { get; private set; }

        private void RegisterContainer()
        {
            AutomapperConfiguration.Initialize();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            this.MainController = container.Resolve<IMainController>();
            this.UserContext = container.Resolve<UserContext>();
        }

        private void LoginBtnClicked(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new ProfilePage(this.MainController, this.UserContext));
            this.email = this.EmailLoginTB.Text;
            this.password = this.PasswordLoginTB.Password.ToString();
            var stackPanel = new StackPanel();

            if (ValidateFields(stackPanel))
            {
                this.loadingWindow = new LoadingWindow()
                {
                    Owner = Application.Current.MainWindow,
                };

                worker.RunWorkerAsync();
                loadingWindow.ShowDialog();
            }
            else
            {
                ErrorDialog.DisplayError(stackPanel, "Log-in failed.");
            }
        }

        private bool ValidateFields(StackPanel stackPanel)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.email))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Please enter an email."));
                isValid = false;
            }
            else
            {
                try
                {
                    MailAddress ma = new MailAddress(this.email);
                }
                catch (SystemException)
                {
                    stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Invalid e-mail."));
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(this.password))
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock("Please enter a password."));
                isValid = false;
            }

            return isValid;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.UserContext.Login(email, password);
            }
            catch (UserException uex)
            {
                throw uex;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingWindow.Close();
            var stackPanel = new StackPanel();

            if (e.Error != null)
            {
                stackPanel.Children.Add(ErrorDialog.CreateErrorTextBlock(e.Error.Message));
                ErrorDialog.DisplayError(stackPanel, "Log-in failed.");
            }
            else
            {
                this.NavigationService.Navigate(new ProfilePage(this.MainController, this.UserContext));
            }
        }

        private void RegisterLinkClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage(this.MainController, this.UserContext));
        }
    }
}
