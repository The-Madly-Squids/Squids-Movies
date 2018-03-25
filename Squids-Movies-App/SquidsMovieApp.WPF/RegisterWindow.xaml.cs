using Bytes2you.Validation;
using SquidsMovieApp.Core.Contracts;
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
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Page
    {
        public RegisterWindow()
        {
            InitializeComponent();
            EmailRegisterTB.Focus();
        }

        private void GoBackBtnClicked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginWindow());
        }

        private void RegisterBtnClicked(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel();

            if (ValidateFields(stackPanel))
            {
                this.NavigationService.Navigate(new ProfileWindow());
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
                stackPanel.Children.Add(CreateEmptyFieldErrorTextBlock("Email"));
                isValid = false;
            }

            if (string.IsNullOrEmpty(username))
            {
                stackPanel.Children.Add(CreateEmptyFieldErrorTextBlock("Username"));
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                stackPanel.Children.Add(CreateEmptyFieldErrorTextBlock("Password"));
                isValid = false;
            }

            if (string.IsNullOrEmpty(repeatedPassword))
            {
                stackPanel.Children.Add(CreateEmptyFieldErrorTextBlock("Repeated password"));
                isValid = false;
            }

            if (password != repeatedPassword)
            {
                var errorTextBlock = new TextBlock();
                errorTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                errorTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                errorTextBlock.FontWeight = FontWeights.Bold;
                errorTextBlock.Text = $"Passwords missmatch.";

                stackPanel.Children.Add(errorTextBlock);
                isValid = false;
            }

            return isValid;
        }

        private TextBlock CreateEmptyFieldErrorTextBlock(string errorElementName)
        {
            var errorTextBlock = new TextBlock();
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            errorTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            errorTextBlock.FontWeight = FontWeights.Bold;
            errorTextBlock.Text = $"{errorElementName} cannot be empty.";

            return errorTextBlock;
        }
    }
}
