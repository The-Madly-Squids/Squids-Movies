using Autofac;
using Bytes2you.Validation;
using SquidsMovieApp.Common.Constants;
using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace SquidsMovieApp.WPF
{
    public partial class RegisterPage : Page
    {
        private readonly IMainController mainController;
        private readonly AuthProvider authProvider;

        public RegisterPage(IMainController mainController, AuthProvider authProvider)
        {
            InitializeComponent();
            EmailRegisterTB.Focus();
            this.mainController = mainController;
            this.authProvider = authProvider;
        }

        private void GoBackBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RegisterBtnClicked(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel();
            var email = this.EmailRegisterTB.Text;
            var username = this.UsernameRegisterTB.Text;
            var password = this.PasswordRegisterPB.Password.ToString();
            var repeatedPassword = this.PasswordRepeatRegisterPB.Password.ToString();

            if (ValidateFields(stackPanel, email, username,password, repeatedPassword))
            {
                mainController.UserController.RegisterUser(username, email, password);

                if (!authProvider.Login(email, password))
                {
                    throw new InvalidOperationException("Could not log-in the registered user");
                }

                this.NavigationService.Navigate(new ProfilePage(this.mainController, this.authProvider));
            }
            else
            {
                var errorWindow = new ErrorWindow(stackPanel)
                {
                    Owner = Application.Current.MainWindow
                };
                errorWindow.ShowDialog();
            }
        }

        private bool ValidateFields(StackPanel stackPanel, string email, string username, 
            string password, string repeatedPassword)
        {
            bool isValid = true;
            
            if (string.IsNullOrEmpty(email))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Email cannot be empty."));
                isValid = false;
            }

            try
            {
                MailAddress ma = new MailAddress(email);
            }
            catch (FormatException)
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Invalid e-mail."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(username))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Username cannot be empty."));
                isValid = false;
            }

            if (username.Length < GlobalConstants.MinUserUsernameLength || GlobalConstants.MaxUserUsernameLength < username.Length)
            {
                stackPanel.Children.Add(CreateErrorTextBlock($"Username must be between {GlobalConstants.MinUserUsernameLength} and {GlobalConstants.MaxUserUsernameLength} symbols long."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Password cannot be empty."));
                isValid = false;
            }

            if (password.Length < GlobalConstants.MinUserPasswordLength)
            {
                stackPanel.Children.Add(CreateErrorTextBlock($"Password must be at least {GlobalConstants.MinUserPasswordLength} symbols."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(repeatedPassword))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Repeated password cannot be empty."));
                isValid = false;
            }

            if (password != repeatedPassword)
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Passwords missmatch."));                
                isValid = false;
            }

            return isValid;
        }

        private TextBlock CreateErrorTextBlock(string errorText)
        {
            var errorTextBlock = new TextBlock();
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            errorTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            errorTextBlock.FontWeight = FontWeights.Bold;
            errorTextBlock.FontSize = 14;
            errorTextBlock.Text = errorText;

            return errorTextBlock;
        }
    }
}
