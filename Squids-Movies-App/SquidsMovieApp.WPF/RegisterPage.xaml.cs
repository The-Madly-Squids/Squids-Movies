using Autofac;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Providers;
using SquidsMovieApp.WPF.Controllers;
using SquidsMovieApp.WPF.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Page loginPage;

        public RegisterPage(IMainController mainController, Page loginPage)
        {
            InitializeComponent();
            EmailRegisterTB.Focus();
            this.mainController = mainController;
            this.loginPage = loginPage;
        }

        private void GoBackBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(loginPage);
        }

        private void RegisterBtnClicked(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel();

            if (ValidateFields(stackPanel))
            {
                this.NavigationService.Navigate(new ProfilePage(this.mainController));
            }
            else
            {
                var errorWindow = new ErrorWindow(stackPanel);
                errorWindow.Owner = Application.Current.MainWindow;
                errorWindow.ShowDialog();
            }
        }

        private bool ValidateFields(StackPanel stackPanel)
        {
            bool isValid = true;
            var email = this.EmailRegisterTB.Text;
            var username = this.UsernameRegisterTB.Text;
            var firstName = this.FirstNamelRegisterTB.Text;
            var lastName = this.LastNameRegisterTB.Text;
            var password = this.PasswordRegisterPB.Password.ToString();
            var repeatedPassword = this.PasswordRepeatRegisterPB.Password.ToString();



            if (string.IsNullOrEmpty(email))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Email cannot be empty."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(username))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Username cannot be empty."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                stackPanel.Children.Add(CreateErrorTextBlock("Password cannot be empty."));
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
